package com.example.cargotruckmobile.Activity.Statistics

import android.graphics.Color
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import com.example.cargotruckmobile.Models.Container
import com.example.cargotruckmobile.R
import com.example.cargotruckmobile.Network.RetrofitClient
import com.github.mikephil.charting.charts.PieChart
import com.github.mikephil.charting.data.PieData
import com.github.mikephil.charting.data.PieDataSet
import com.github.mikephil.charting.data.PieEntry
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class ContainerStatisticsActivity : AppCompatActivity() {

    private lateinit var pieChart: PieChart

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_container_statistics)

        // Знаходимо EditText, TextView та PieChart у макеті
        val containerIdInput: EditText = findViewById(R.id.containerIdInput)
        val containerNameTextView: TextView = findViewById(R.id.containerName)
        val containerTypeTextView: TextView = findViewById(R.id.containerType)
        val numberTripsTextView: TextView = findViewById(R.id.numberTrips)


        val pieChart: PieChart = findViewById(R.id.pieChart)

        // Обробник подій для автоматичного пошуку даних
        containerIdInput.addTextChangedListener(object : TextWatcher {
            override fun beforeTextChanged(s: CharSequence?, start: Int, count: Int, after: Int) {}

            override fun onTextChanged(s: CharSequence?, start: Int, before: Int, count: Int) {
                val containerId = s.toString()
                if (containerId.isNotEmpty()) {
                    // Виклик функції для отримання даних
                    fetchContainerStatistics(containerId, containerNameTextView, containerTypeTextView, numberTripsTextView, pieChart)
                }
            }

            override fun afterTextChanged(s: Editable?) {}
        })
    }

    private fun fetchContainerStatistics(
        containerId: String,
        containerNameTextView: TextView,
        containerTypeTextView: TextView,
        numberTripsTextView: TextView,
        pieChart: PieChart
    ) {
        lifecycleScope.launch(Dispatchers.Main) {
            try {
                // Виконуємо запит до сервера
                val response = RetrofitClient.statisticsService.getContainerStatistics(containerId)

                // Встановлюємо текст у TextView
                containerNameTextView.text = "Container Name: ${response.containerName}"
                containerTypeTextView.text = "Container Type: ${response.containerType}"
                numberTripsTextView.text = "Number of Trips: ${response.numberTrips}"


                // Створення списку для PieEntry
                val entries = ArrayList<PieEntry>()
                entries.add(PieEntry(response.averageLoadCapacity.toFloat(), "Load Capacity"))
                entries.add(PieEntry(response.volumetricWeight.toFloat(), "Volumetric Weight"))

                // Створення DataSet і PieData
                val dataSet = PieDataSet(entries, "Container Statistics")

// Налаштування кольорів для кожного значення
                val colors = listOf(Color.RED, Color.BLUE)
                dataSet.colors = colors

// Зміна кольору тексту значень
                dataSet.valueTextColor = Color.BLACK

// Задання розміру тексту значень
                dataSet.valueTextSize = 18f
                val data = PieData(dataSet)

                // Налаштування PieChart
                pieChart.data = data
                pieChart.description.isEnabled = false
                pieChart.invalidate() // Оновлення діаграми

            } catch (e: Exception) {
                Toast.makeText(this@ContainerStatisticsActivity, "Container not found", Toast.LENGTH_SHORT).show()
                clearChart(pieChart)
                clearTextViews(containerNameTextView,containerTypeTextView,numberTripsTextView)
            }
        }
    }

    private fun setupPieChart(containerData: Container) {
        val entries = listOf(
            PieEntry(containerData.averageLoadCapacity.toFloat(), "Average Load Capacity"),
            PieEntry(containerData.volumetricWeight.toFloat(), "Volumetric Weight")
        )

        val dataSet = PieDataSet(entries, "Container Statistics")
        val data = PieData(dataSet)
        pieChart.data = data
        pieChart.invalidate() // Оновлює діаграму
    }
    private fun clearChart(pieChart: PieChart) {
        pieChart.clear()
    }

    private fun clearTextViews(vararg textViews: TextView) {
        textViews.forEach { it.text = "" }
    }
}

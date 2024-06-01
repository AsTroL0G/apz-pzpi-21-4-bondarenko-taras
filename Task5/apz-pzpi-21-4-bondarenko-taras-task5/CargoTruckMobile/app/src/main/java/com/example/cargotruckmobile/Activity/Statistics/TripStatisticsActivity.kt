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
import com.example.cargotruckmobile.R
import com.example.cargotruckmobile.Network.RetrofitClient
import com.github.mikephil.charting.charts.BarChart
import com.github.mikephil.charting.data.BarData
import com.github.mikephil.charting.data.BarDataSet
import com.github.mikephil.charting.data.BarEntry
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class TripStatisticsActivity : AppCompatActivity() {

    private lateinit var barChart: BarChart

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_trip_statistics)

        // Знаходимо EditText, TextView та BarChart у макеті
        val tripIdInput: EditText = findViewById(R.id.tripIdInput)
        val containerNameTextView: TextView = findViewById(R.id.containerName)
        val containerTypeTextView: TextView = findViewById(R.id.containerType)
        val totalWeightTextView: TextView = findViewById(R.id.totalWeight)
        val barChart: BarChart = findViewById(R.id.barChart)
        val timeTextView: TextView = findViewById(R.id.time)

        // Обробник подій для автоматичного пошуку даних
        tripIdInput.addTextChangedListener(object : TextWatcher {
            override fun beforeTextChanged(s: CharSequence?, start: Int, count: Int, after: Int) {}

            override fun onTextChanged(s: CharSequence?, start: Int, before: Int, count: Int) {
                val tripId = s.toString()
                if (tripId.isNotEmpty()) {
                    // Виклик функції для отримання даних
                    fetchTripStatistics(tripId, containerNameTextView, containerTypeTextView, totalWeightTextView,timeTextView, barChart)
                }
            }

            override fun afterTextChanged(s: Editable?) {}
        })
    }

    private fun fetchTripStatistics(
        tripId: String,
        containerNameTextView: TextView,
        containerTypeTextView: TextView,
        totalWeightView: TextView,
        timeTextView: TextView,
        barChart: BarChart
    ) {
        lifecycleScope.launch(Dispatchers.Main) {
            try {
                // Виконуємо запит до сервера
                val response = RetrofitClient.statisticsService.getTripStatistics(tripId)

                containerNameTextView.text = "Container Name: ${response.containerName}"
                containerTypeTextView.text = "Container Type: ${response.containerType}"
                totalWeightView.text = "Averege speed: ${response.averageSpeed}"
                timeTextView.text = "Time: ${response.timeSpent}"

                val entries = ArrayList<BarEntry>()
                entries.add(BarEntry(0f, response.totalWeight.toFloat()))

                val dataSet = BarDataSet(entries, "Total weight")

                val colors = listOf(Color.RED, Color.BLUE)
                dataSet.colors = colors

                dataSet.valueTextColor = Color.BLACK

                dataSet.valueTextSize = 18f
                val data = BarData(dataSet)

                barChart.data = data
                barChart.description.isEnabled = false
                barChart.invalidate() // Оновлення діаграми

            } catch (e: Exception) {
                Toast.makeText(this@TripStatisticsActivity, "Container not found", Toast.LENGTH_SHORT).show()
                clearChart(barChart)
                clearTextViews(containerNameTextView, containerTypeTextView, totalWeightView)
            }
        }
    }

    private fun clearChart(barChart: BarChart) {
        barChart.clear()
    }

    private fun clearTextViews(vararg textViews: TextView) {
        textViews.forEach { it.text = "" }
    }
}

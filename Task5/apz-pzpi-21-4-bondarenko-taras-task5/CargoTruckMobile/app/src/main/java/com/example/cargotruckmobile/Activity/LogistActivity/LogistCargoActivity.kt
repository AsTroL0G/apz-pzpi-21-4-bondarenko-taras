package com.example.cargotruckmobile.Activity.LogistActivity

import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.cargotruckmobile.Activity.Adapter.LogistCargoAdapter
import com.example.cargotruckmobile.Models.Cargo
import com.example.cargotruckmobile.R
import com.example.cargotruckmobile.Network.RetrofitClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class LogistCargoActivity : AppCompatActivity() {

    private lateinit var cargoList: RecyclerView
    private val cargos = arrayListOf<Cargo>()
    private lateinit var adapter: LogistCargoAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_logist_cargo)

        cargoList = findViewById(R.id.cargoList)
        cargoList.layoutManager = LinearLayoutManager(this)
        adapter = LogistCargoAdapter(cargos, this)
        cargoList.adapter = adapter
        val sharedPreferences = getSharedPreferences("user_prefs", MODE_PRIVATE)
        val userId = sharedPreferences.getString("userId", null).toString()

        fetchCargoData(userId)
    }

    private fun fetchCargoData(id: String) {
        lifecycleScope.launch(Dispatchers.Main) {
            try {
                val cargoList = RetrofitClient.cargoService.getCargoLogist()

                cargos.clear()
                cargos.addAll(cargoList.items)
                adapter.notifyDataSetChanged()
                Log.d("Cargo", "Data fetched successfully: ${cargoList.items.size} items")
            } catch (e: Exception) {
                Log.e("Error", "Failed to fetch cargo: ${e.message}")
            }
        }
    }

}
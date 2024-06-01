package com.example.cargotruckmobile.Activity.Adapter

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.TextView
import com.example.cargotruckmobile.Models.Cargo
import com.example.cargotruckmobile.R

class AdminAdapter(
    private val context: Context,
    private val cargo: List<Cargo>,
    private val onSelectClick: (String) -> Unit
) : ArrayAdapter<Cargo>(context, 0, cargo) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val view = convertView ?: LayoutInflater.from(context).inflate(R.layout.cargo_item, parent, false)
        val cargoItem = getItem(position)

        val tvCargoId = view.findViewById<TextView>(R.id.tvCargoId)
        val tvCargoName = view.findViewById<TextView>(R.id.tvCargoName)
        val tvCargoStatus = view.findViewById<TextView>(R.id.tvCargoStatus)

        cargoItem?.let {
            tvCargoId.text = it.id
            tvCargoName.text = it.name
            tvCargoStatus.text = it.status
            view.setOnClickListener { onSelectClick(it.id.toString()) }
        }

        return view
    }
}

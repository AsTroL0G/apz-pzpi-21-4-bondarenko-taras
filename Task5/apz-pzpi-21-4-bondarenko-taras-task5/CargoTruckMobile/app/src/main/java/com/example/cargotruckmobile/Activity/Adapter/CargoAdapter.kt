package com.example.cargotruckmobile.Activity.Adapter

import android.content.Context
import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.LinearLayout
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.cargotruckmobile.Activity.UserCargoActivity.CargoActivity
import com.example.cargotruckmobile.Models.Cargo
import com.example.cargotruckmobile.R

class CargoAdapter (var items: List<Cargo>, var context: Context): RecyclerView.Adapter<CargoAdapter.MyViewHolder>() {

    class MyViewHolder(view: View): RecyclerView.ViewHolder(view){
        val cargoForm: LinearLayout = view.findViewById(R.id.cargoForm)
        val cargoName: TextView = view.findViewById(R.id.cargoName)
        val cargoId: TextView = view.findViewById(R.id.cargoId)
        val cargoStatus: TextView = view.findViewById(R.id.cargoStatus)
        val cargoCityReceiver: TextView = view.findViewById(R.id.cargoCityReceiver)
        val cargoCitySender: TextView = view.findViewById(R.id.cargoCitySender)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder {
        val view= LayoutInflater.from(parent.context).inflate(R.layout.cargo_in_list,parent, false)
        return MyViewHolder(view)
    }

    override fun getItemCount(): Int {
        return  items.count()
    }

    override fun onBindViewHolder(holder: MyViewHolder, position: Int) {
        holder.cargoId.text = items[position].id
        holder.cargoName.text = items[position].name
        holder.cargoStatus.text = items[position].status
        holder.cargoCitySender.text = items[position].citySenderId
        holder.cargoCityReceiver.text = items[position].cityReceiverId

        holder.cargoForm.setOnClickListener{

            val intent = Intent(context, CargoActivity::class.java)
            intent.putExtra("cargoId", items[position].id)
            context.startActivity(intent)
        }
    }
}
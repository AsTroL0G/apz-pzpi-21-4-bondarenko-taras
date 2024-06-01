package com.example.cargotruckmobile.Activity.Adapter

import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.LinearLayout
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.FragmentActivity
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.RecyclerView
import com.example.cargotruckmobile.Activity.LogistActivity.CreateTripDialog
import com.example.cargotruckmobile.Activity.UserCargoActivity.CargoActivity
import com.example.cargotruckmobile.Models.Cargo
import com.example.cargotruckmobile.R
import com.example.cargotruckmobile.Network.RetrofitClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class LogistCargoAdapter(private val items: List<Cargo>, private val activity: FragmentActivity): RecyclerView.Adapter<LogistCargoAdapter.MyViewHolder>() {

    class MyViewHolder(view: View): RecyclerView.ViewHolder(view){
        val cargoForm: LinearLayout = view.findViewById(R.id.cargoFormLogist)
        val cargoName: TextView = view.findViewById(R.id.cargoNameTextView)
        val cargoId: TextView = view.findViewById(R.id.cargoIdTextView)
        val cargoStatus: TextView = view.findViewById(R.id.cargoStatusTextView)
        val createTrip: Button = view.findViewById(R.id.btnCreateJourney)
        val createShedule: Button = view.findViewById(R.id.btnCreateSchedule)
        val createNotice: Button = view.findViewById(R.id.btnCreateManifest)
        val notice: TextView = view.findViewById(R.id.et_first_name)
        val submit: Button = view.findViewById(R.id.btnCreateCargo)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.item_cargo, parent, false)
        return MyViewHolder(view)
    }

    override fun getItemCount(): Int {
        return items.size
    }

    override fun onBindViewHolder(holder: MyViewHolder, position: Int) {
        holder.cargoId.text = items[position].id
        holder.cargoName.text = items[position].name
        holder.cargoStatus.text = items[position].status

        holder.cargoForm.setOnClickListener {
            val intent = Intent(activity, CargoActivity::class.java)
            intent.putExtra("cargoId", items[position].id)
            activity.startActivity(intent)
        }

        holder.createTrip.setOnClickListener {
            val fragmentManager = activity.supportFragmentManager
            val createTripDialog = CreateTripDialog()
            createTripDialog.show(fragmentManager, "CreateTripDialog")
        }

        holder.submit.setOnClickListener {
            val noticeId = holder.notice.text.toString()
            activity.lifecycleScope.launch(Dispatchers.IO) {
                try {
                    RetrofitClient.cargoService.updateNotice(items[position].id, noticeId)
                    withContext(Dispatchers.Main) {
                        notifyDataSetChanged() // or notifyItemChanged(position) if you want to update a specific item
                    }
                } catch (e: Exception) {
                    Toast.makeText(activity, "An error occurred: ${e.message}", Toast.LENGTH_SHORT).show()
                }
            }
        }
    }
}

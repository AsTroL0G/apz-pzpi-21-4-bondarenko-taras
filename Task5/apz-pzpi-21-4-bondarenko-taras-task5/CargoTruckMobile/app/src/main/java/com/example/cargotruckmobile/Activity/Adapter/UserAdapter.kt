package com.example.cargotruckmobile.Activity.Adapter

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.Button
import android.widget.TextView
import com.example.cargotruckmobile.Models.UserDto
import com.example.cargotruckmobile.R

class UserAdapter(
    private val context: Context,
    private val users: List<UserDto>,
    private val onBlockClick: (String) -> Unit,
    private val onUnblockClick: (String) -> Unit
) : ArrayAdapter<UserDto>(context, 0, users) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val view = convertView ?: LayoutInflater.from(context).inflate(R.layout.user_item, parent, false)
        val user = getItem(position)

        val tvUserId = view.findViewById<TextView>(R.id.tvUserId)
        val tvUserName = view.findViewById<TextView>(R.id.tvUserName)
        val tvUserEmail = view.findViewById<TextView>(R.id.tvUserEmail)
        val btnBlock = view.findViewById<Button>(R.id.btnBlock)
        val btnUnblock = view.findViewById<Button>(R.id.btnUnblock)

        user?.let {
            tvUserId.text = it.id
            tvUserName.text = "${it.firstName} ${it.lastName}"
            tvUserEmail.text = it.email

            btnBlock.setOnClickListener { onBlockClick(it.id.toString()) }
            btnUnblock.setOnClickListener { onUnblockClick(it.id.toString()) }
        }

        return view
    }
}

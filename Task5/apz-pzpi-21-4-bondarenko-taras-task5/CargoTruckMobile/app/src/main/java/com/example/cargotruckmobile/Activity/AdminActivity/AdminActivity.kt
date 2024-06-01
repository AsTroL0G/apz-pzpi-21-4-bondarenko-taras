package com.example.cargotruckmobile.Activity.AdminActivity

import android.os.Bundle
import android.os.Environment
import android.util.Log
import android.view.View
import android.widget.Button
import android.widget.ListView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import com.example.cargotruckmobile.Activity.Adapter.AdminAdapter
import com.example.cargotruckmobile.Activity.Adapter.UserAdapter
import com.example.cargotruckmobile.Models.Cargo
import com.example.cargotruckmobile.Models.UserDto
import com.example.cargotruckmobile.R
import com.example.cargotruckmobile.Network.RetrofitClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.ResponseBody
import java.io.File
import java.io.FileOutputStream
import java.io.InputStream
import java.io.OutputStream

class AdminActivity : AppCompatActivity() {

    private lateinit var btnUser: Button
    private lateinit var btnCargo: Button
    private lateinit var entityListView: ListView
    private lateinit var btnCreateBackup: Button
    private lateinit var entity: String

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_admin)

        btnUser = findViewById(R.id.btnUser)
        btnCargo = findViewById(R.id.btnCargo)
        entityListView = findViewById(R.id.entityListView)
        btnCreateBackup = findViewById(R.id.btnCreateBackup)

        btnUser.setOnClickListener {
            fetchUsers()
        }
        btnCargo.setOnClickListener {
            fetchCargo()
        }
        btnCreateBackup.setOnClickListener {
            createBackup(entity)
        }
    }

    private fun fetchUsers() {
        lifecycleScope.launch(Dispatchers.Main) {
            try {
                val users = RetrofitClient.userService.getUser()
                entity = "User"
                displayUsers(users.items)
                Log.d("Cargo", "Data fetched successfully: ${users.items.size} items")
            } catch (e: Exception) {
                Log.e("Error", "Failed to fetch cargo: ${e.message}")
            }
        }
    }

    private fun fetchCargo() {
        lifecycleScope.launch(Dispatchers.Main) {
            try {
                val cargo = RetrofitClient.cargoService.getCargoPage()
                entity = "User"
                displayCargo(cargo.items)
                Log.d("Cargo", "Data fetched successfully: ${cargo.items.size} items")
            } catch (e: Exception) {
                Log.e("Error", "Failed to fetch cargo: ${e.message}")
            }
        }
    }

    private fun displayUsers(users: List<UserDto>) {
        val adapter = UserAdapter(
            this,
            users,
            onBlockClick = { userId -> blockUser(userId) },
            onUnblockClick = { userId -> unblockUser(userId) }
        )
        entityListView.adapter = adapter
        entityListView.visibility = View.VISIBLE
        btnCreateBackup.visibility = View.VISIBLE
    }
    private fun displayCargo(cargo: List<Cargo>) {
        val adapter = AdminAdapter(
            this,
            cargo,
            onSelectClick = { cargoItem -> selectCargo(cargoItem) }
        )
        entityListView.adapter = adapter
        entityListView.visibility = View.VISIBLE
        btnCreateBackup.visibility = View.VISIBLE
    }
    private fun selectCargo(cargoId: String) {
        // Implement logic for cargo selection if needed
        Toast.makeText(this, "Selected cargo with ID: $cargoId", Toast.LENGTH_SHORT).show()
    }
    private fun blockUser(userId: String) {

        lifecycleScope.launch(Dispatchers.Main) {
            try {
                val users = RetrofitClient.userService.banUser(userId)
            } catch (e: Exception) {
                Log.e("Error", "Failed to fetch cargo: ${e.message}")
            }
        }


        Toast.makeText(this, "Blocked user with ID: $userId", Toast.LENGTH_SHORT).show()
    }

    private fun unblockUser(userId: String) {
        // Implement the logic to unblock the user here
        Toast.makeText(this, "Unblocked user with ID: $userId", Toast.LENGTH_SHORT).show()

        lifecycleScope.launch(Dispatchers.Main) {
            try {
                val users = RetrofitClient.userService.unBanUser(userId)
            } catch (e: Exception) {
                Log.e("Error", "Failed to fetch cargo: ${e.message}")
            }
        }
    }

    private fun createBackup(entity: String) {
        lifecycleScope.launch(Dispatchers.IO) {
            try {
                val responseBody = RetrofitClient.exportService.downloadBackup(entity)
                val file = saveFile(responseBody, "backup_$entity.csv")

                launch(Dispatchers.Main) {
                    if (file != null) {
                        Toast.makeText(this@AdminActivity, "File saved: ${file.absolutePath}", Toast.LENGTH_LONG).show()
                        Log.d("Backup", "File saved at: ${file.absolutePath}")
                    } else {
                        Toast.makeText(this@AdminActivity, "Failed to save file", Toast.LENGTH_SHORT).show()
                    }
                }
            } catch (e: Exception) {
                Log.e("Error", "Failed to create backup: ${e.message}")
            }
        }
    }

    private fun saveFile(body: ResponseBody, fileName: String): File? {
        return try {
            val filePath = File(getExternalFilesDir(Environment.DIRECTORY_DOWNLOADS), fileName)
            var inputStream : InputStream ? = null
            var outputStream : OutputStream ? = null

            try {
                inputStream = body.byteStream()
                outputStream = FileOutputStream(filePath)

                val fileReader = ByteArray(4096)
                var fileSizeDownloaded: Long = 0

                while (true) {
                    val read = inputStream.read(fileReader)
                    if (read == -1) {
                        break
                    }

                    outputStream.write(fileReader, 0, read)
                    fileSizeDownloaded += read
                }

                outputStream.flush()
                filePath
            } catch (e: Exception) {
                Log.e("Error", "Error saving file: ${e.message}")
                null
            } finally {
                inputStream?.close()
                outputStream?.close()
            }
        } catch (e: Exception) {
            Log.e("Error", "Error saving file: ${e.message}")
            null
        }
    }
}

package com.example.drivingschoolappandroidclient.ui.login

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.core.widget.doAfterTextChanged
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.NavigationDrawerActivity
import com.example.drivingschoolappandroidclient.databinding.LoginActivityBinding
import com.example.drivingschoolappandroidclient.models.models.LoginModel
import com.example.drivingschoolappandroidclient.models.models.LoginResponse
import com.example.drivingschoolappandroidclient.models.models.MyCallback
import com.example.drivingschoolappandroidclient.models.models.MyResponse
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import retrofit2.Call
import retrofit2.Response


class LoginActivity : AppCompatActivity(){

    private lateinit var binding: LoginActivityBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = LoginActivityBinding.inflate(layoutInflater)

        CoroutineScope(Dispatchers.IO).launch {
            App.getLoginResponse(this@LoginActivity)?.let{
                val validToken = tryLogin(it.authHead)
                withContext(Dispatchers.Main) {
                    if (validToken)
                        login(it)
                    else
                        App.loginSaveToPreferences(null, this@LoginActivity)
                }
            }
        }

        with(binding){
            setContentView(root)

            App.blocked.observe(this@LoginActivity){
                if(it){
                    App.showWaitingScreen(window,layoutInflater)
                } else {
                    App.hideWaitingScreen(window)
                }
            }
            etEmail.doAfterTextChanged { onChanged() }
            etPassword.doAfterTextChanged { onChanged() }
            onChanged()

            btnLogin.setOnClickListener {
                App.block()
                val model = LoginModel(
                    email = etEmail.text.toString(),
                    password = etPassword.text.toString()
                )
                App.controller.api.login(model).enqueue(
                    LoginCallback(this@LoginActivity)
                )
            }

        }
    }

    private fun onChanged() {
        with(binding){
            btnLogin.isEnabled = etEmail.text.isNotEmpty() && etPassword.text.isNotEmpty()
        }
    }

    override fun onDestroy() {
        App.blocked.removeObservers(this)
        super.onDestroy()
    }

    fun login(loginResponse: LoginResponse) {
        App.controller.loginResponse.value = loginResponse
        App.loginSaveToPreferences(loginResponse, this)
        val intent = Intent(this@LoginActivity,NavigationDrawerActivity::class.java)
        startActivity(intent)
        finish()
    }

    private fun tryLogin(authHead: String) : Boolean {
        return try{
            val e = App.controller.api.ping(authHead).execute()
            e.body()?.let { it.`package`?.equals(true) } ?: false
        } catch (e: Exception){
            false
        }
    }

}

class LoginCallback(val loginActivity: LoginActivity) : MyCallback<MyResponse<LoginResponse?>>() {
    override fun onResponse(
        p0: Call<MyResponse<LoginResponse?>>,
        p1: Response<MyResponse<LoginResponse?>>
    ) {
        default(p0,p1,"Login"){
            val response = p1.body()
            if(response?.`package` == null){
                Toast.makeText(
                    loginActivity,
                    "Ошибка входа",
                    Toast.LENGTH_LONG
                ).show()
                return@default
            }
            loginActivity.login(response.`package`)
        }
    }
}
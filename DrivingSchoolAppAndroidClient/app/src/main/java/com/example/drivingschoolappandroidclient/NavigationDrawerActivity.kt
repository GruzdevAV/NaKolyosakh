package com.example.drivingschoolappandroidclient

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.drawerlayout.widget.DrawerLayout
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.navigateUp
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.navigation.ui.setupWithNavController
import com.example.drivingschoolappandroidclient.databinding.ActivityNavigationDrawerBinding
import com.example.drivingschoolappandroidclient.databinding.NavHeaderNavigationDrawerBinding
import com.example.drivingschoolappandroidclient.models.models.UserRoles
import com.example.drivingschoolappandroidclient.ui.login.LoginActivity
import com.google.android.material.navigation.NavigationView
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class NavigationDrawerActivity : AppCompatActivity() {

    private lateinit var appBarConfiguration: AppBarConfiguration
    private lateinit var binding: ActivityNavigationDrawerBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityNavigationDrawerBinding.inflate(layoutInflater)
        setContentView(binding.root)

        setSupportActionBar(binding.appBarNavigationDrawer.toolbar)

//        binding.appBarNavigationDrawer.fab.setOnClickListener { view ->
//            Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
//                .setAction("Action", null).show()
//        }
        loadData()
        val drawerLayout: DrawerLayout = binding.drawerLayout
        val navView: NavigationView = binding.navView
        val header = NavHeaderNavigationDrawerBinding.bind(navView.getHeaderView(0))
        App.me.observe(this){
            header.ivHeaderPicture.setImageBitmap(
                it?.profilePicture
            )
            header.tvHeaderMain.text = it?.toString()
            header.tvHeaderSub.text = it?.email
        }
        App.blocked.observe(this){
            if(it){
                App.showLoadingScreen(window,layoutInflater)
            } else {
                App.hideLoadingScreen(window)
            }
        }
        App.controller.loginResponse.observe(this){
            App.loginSaveToPreferences(it, this)
            if(it == null){
                exit()
                return@observe
            }
            when (it.role){
                UserRoles.admin -> {
                    navView.menu.findItem(R.id.nav_grades).isVisible = false
                    navView.menu.findItem(R.id.nav_register_user).isVisible = true
                }
                UserRoles.instructor -> {
                    navView.menu.findItem(R.id.nav_grades).isVisible = true
                    navView.menu.findItem(R.id.nav_register_user).isVisible = false
                }
                UserRoles.student -> {
                    navView.menu.findItem(R.id.nav_grades).isVisible = true
                    navView.menu.findItem(R.id.nav_register_user).isVisible = false
                }
            }
        }
        App.response.observe(this){
            it?.let {
                val mes = "${it.status}:\n${it.message}"
                Toast.makeText(this,mes,Toast.LENGTH_LONG).show()
            }
        }
        val navController = findNavController(R.id.nav_host_fragment_content_navigation_drawer)
        val menuSet = getMenuSet()
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        appBarConfiguration = AppBarConfiguration(
            menuSet, drawerLayout
        )
        setupActionBarWithNavController(navController, appBarConfiguration)
        navView.setupWithNavController(navController)
    }

    private fun exit() {
        val intent = Intent(this,LoginActivity::class.java)
        App.loginSaveToPreferences(null, this)
        startActivity(intent)
        finish()
    }

    private fun loadData() {
        CoroutineScope(Dispatchers.IO).launch {
            with(App){
                reloadMe()
                reloadClasses()
                reloadInnerSchedules()
                reloadStudents()
                reloadInstructors()
                reloadGrades()
            }
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        App.me.removeObservers(this)
        App.blocked.removeObservers(this)
        App.response.removeObservers(this)
    }
    private fun getMenuSet(): Set<Int> {

        val result = mutableSetOf(
            R.id.nav_home,
            R.id.nav_profile, R.id.nav_register_user, R.id.nav_students,
            R.id.nav_instructors, R.id.nav_schedules,
            R.id.nav_classes, R.id.nav_grades
        )
        when (App.controller.loginResponse.value!!.role){
            UserRoles.admin -> result.remove(R.id.nav_grades)
            UserRoles.instructor -> result.remove(R.id.nav_register_user)
            UserRoles.student -> result.remove(R.id.nav_register_user)
        }
        return result
    }

//    override fun onCreateOptionsMenu(menu: Menu): Boolean {
//        // Inflate the menu; this adds items to the action bar if it is present.
//        menuInflater.inflate(R.menu.navigation_drawer, menu)
//        return true
//    }

    override fun onSupportNavigateUp(): Boolean {
        val navController = findNavController(R.id.nav_host_fragment_content_navigation_drawer)
        return navController.navigateUp(appBarConfiguration) || super.onSupportNavigateUp()
    }

}
package com.example.drivingschoolappandroidclient.ui.schedules

import android.widget.ArrayAdapter
import androidx.lifecycle.ViewModel
import com.example.drivingschoolappandroidclient.models.models.Instructor

class AddInnerScheduleViewModel : ViewModel() {
    var instructorAdapter : ArrayAdapter<Instructor>? = null
}
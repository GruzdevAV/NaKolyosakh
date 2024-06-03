package com.example.drivingschoolappandroidclient.ui.students

import android.widget.ArrayAdapter
import androidx.lifecycle.ViewModel
import com.example.drivingschoolappandroidclient.models.models.Instructor

class StudentsViewModel : ViewModel() {
    val studentsAdapter = StudentAdapter()
    var instructorAdapter : ArrayAdapter<Instructor>? = null
}
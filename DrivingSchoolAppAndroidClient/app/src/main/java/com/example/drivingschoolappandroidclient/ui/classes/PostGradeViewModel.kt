package com.example.drivingschoolappandroidclient.ui.classes

import android.widget.ArrayAdapter
import androidx.lifecycle.ViewModel
import com.example.drivingschoolappandroidclient.models.models.GradesByInstructorsToStudents
import com.example.drivingschoolappandroidclient.models.models.GradesByStudentsToInstructors

class PostGradeViewModel : ViewModel() {
    var gradesByStudentAdapter : ArrayAdapter<GradesByStudentsToInstructors>? = null
    var gradesByInstructorAdapter : ArrayAdapter<GradesByInstructorsToStudents>? = null
}
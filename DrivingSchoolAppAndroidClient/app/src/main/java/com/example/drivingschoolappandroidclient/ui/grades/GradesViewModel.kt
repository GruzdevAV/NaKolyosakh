package com.example.drivingschoolappandroidclient.ui.grades

import androidx.lifecycle.ViewModel

class GradesViewModel : ViewModel() {
    val gradesByInstructorsAdapter = GradesByInstructorsAdapter()
    val gradesByStudentsAdapter = GradesByStudentsAdapter()
}
package com.example.drivingschoolappandroidclient.models.models

open class OuterScheduleOfInstructorModel(
    val googleSheetId: String,
    val googleSheetPageName: String,
    val timesOfClassesRange: String,
    val datesOfClassesRange: String,
    val classesRange: String,
    val freeClassExampleRange: String,
    val notFreeClassExampleRange: String?,
    val yearRange: String
)


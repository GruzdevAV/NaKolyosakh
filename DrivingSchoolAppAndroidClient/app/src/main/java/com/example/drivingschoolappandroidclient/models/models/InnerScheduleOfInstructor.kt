package com.example.drivingschoolappandroidclient.models.models

import com.example.drivingschoolappandroidclient.App
import java.time.LocalDate

open class InnerScheduleOfInstructor(
var innerScheduleOfInstructorId : Int,
var instructorId : Int,
var instructor : Instructor,
var dayOfWorkJson : String,
var outerScheduleId : Int?,
var outerScheduleOfInstructor : OuterScheduleOfInstructor?,

){
    var dayOfWork : LocalDate
        get() = App.dateFromString(dayOfWorkJson)
        set(value) {
            dayOfWorkJson = App.dateToString(value)
        }
    val IsOuterSchedule = outerScheduleId != null
}

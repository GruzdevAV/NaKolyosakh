package com.example.drivingschoolappandroidclient.models.models

import com.example.drivingschoolappandroidclient.App
import java.time.LocalTime

data class ClassModel(
    var studentId: Int? = null,
    var startTimeJson: String,
    var durationJson: String,
    var instructorId: Int? = null,
    var innerScheduleOfInstructorId: Int? = null
){
    var startTime: LocalTime
        get() = App.timeFromString(startTimeJson)
        set(value) {
            startTimeJson = App.timeToString(value)
        }
    var duration: LocalTime
        get() = App.timeFromString(durationJson)
        set(value) {
            durationJson = App.timeToString(value)
        }
}
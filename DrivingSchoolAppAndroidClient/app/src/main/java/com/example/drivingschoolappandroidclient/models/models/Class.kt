package com.example.drivingschoolappandroidclient.models.models

import com.example.drivingschoolappandroidclient.App
import java.time.LocalTime

data class Class(
    var classId: Int,
    var innerScheduleOfInstructorId: Int,
    var innerScheduleOfInstructor: InnerScheduleOfInstructor,
    var studentId: Int?,
    var student: Student?,
    var startTimeJson: String,
    var durationJson: String,
    var status : Byte
){
    var classStatus : ClassStatus
        get() = ClassStatus.ofValue(status)
        set(value) {status = value.value}
    var startTime: LocalTime
        get() = App.timeFromString(startTimeJson)
        set(value){
            startTimeJson = App.timeToString(value)
        }
    var duration: LocalTime
        get() = App.timeFromString(durationJson)
        set(value){
            durationJson = App.timeToString(value)
        }
    val isOuterClass get() = innerScheduleOfInstructor.IsOuterSchedule;
    val outerScheduleId get() = innerScheduleOfInstructor.outerScheduleId
    val outerScheduleOfInstructor get() = innerScheduleOfInstructor.outerScheduleOfInstructor
    var endTime: LocalTime
        get() = startTime.plusSeconds(duration.toSecondOfDay().toLong())
        set(value) {
            duration = value.minusSeconds(startTime.toSecondOfDay().toLong())
        }
    val startDateTime get() = innerScheduleOfInstructor.dayOfWork.atTime(startTime)
    val endDateTime get() = innerScheduleOfInstructor.dayOfWork.atTime(endTime)
    val date get() = innerScheduleOfInstructor.dayOfWork
    val instructorId get() = instructor.instructorId
    val instructor get() = innerScheduleOfInstructor.instructor
    override fun toString() =
        "$date $startTime-$endTime"

}

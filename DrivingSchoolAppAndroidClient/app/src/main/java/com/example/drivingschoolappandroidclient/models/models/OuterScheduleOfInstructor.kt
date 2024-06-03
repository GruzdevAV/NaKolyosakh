package com.example.drivingschoolappandroidclient.models.models

class OuterScheduleOfInstructor(
    googleSheetId: String,
    googleSheetPageName: String,
    timesOfClassesRange: String,
    datesOfClassesRange: String,
    classesRange: String,
    freeClassExampleRange: String,
    notFreeClassExampleRange: String?,
    yearRange: String

) : OuterScheduleOfInstructorModel(
    googleSheetId,
    googleSheetPageName,
    timesOfClassesRange,
    datesOfClassesRange,
    classesRange,
    freeClassExampleRange,
    notFreeClassExampleRange,
    yearRange
){
    /**
     * Id внешнего расписания
     */
    var outerScheduleId : Int? = null

    /**
     * Id инструктора, которому принадлежит это внешнее расписание
     */
    var instructorId : Int? = null
    /**
     * Инструктор, которому принадлежит это внешнее расписание
     */
    var instructor : Instructor? = null

    companion object {
        fun constructor(model : OuterScheduleOfInstructorModel) = OuterScheduleOfInstructor(
            googleSheetId = model.googleSheetId,
            googleSheetPageName = model.googleSheetPageName,
            timesOfClassesRange = model.timesOfClassesRange,
            datesOfClassesRange = model.datesOfClassesRange,
            classesRange = model.classesRange,
            freeClassExampleRange = model.freeClassExampleRange,
            notFreeClassExampleRange = model.notFreeClassExampleRange,
            yearRange = model.yearRange
        )
    }
    // TODO: добавить все методы по получению данных, добавлению и проверке внутренних расписаний сюда
}



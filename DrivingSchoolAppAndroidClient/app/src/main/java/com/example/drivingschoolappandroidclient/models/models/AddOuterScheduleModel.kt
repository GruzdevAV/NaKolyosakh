package com.example.drivingschoolappandroidclient.models.models

class AddOuterScheduleModel(
    val userId: String,
    googleSheetId: String,
    googleSheetPageName: String,
    timesOfClassesRange: String,
    datesOfClassesRange: String,
    classesRange: String,
    freeClassExampleRange: String,
    notFreeClassExampleRange: String?,
    yearRange: String,
) : OuterScheduleOfInstructorModel(
    googleSheetId,
    googleSheetPageName,
    timesOfClassesRange,
    datesOfClassesRange,
    classesRange,
    freeClassExampleRange,
    notFreeClassExampleRange,
    yearRange
)

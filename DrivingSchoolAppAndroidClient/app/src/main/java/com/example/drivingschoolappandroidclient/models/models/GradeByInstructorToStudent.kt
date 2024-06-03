package com.example.drivingschoolappandroidclient.models.models

import com.google.gson.annotations.SerializedName

class GradeByInstructorToStudent(
    classId: Int,
    grade: Byte,
    comment: String?,
    @SerializedName("class")
    val _class: Class
) : GradeByInstructorToStudentModel(classId, grade, comment) {
}
package com.example.drivingschoolappandroidclient.models.models

/**
Модель для передачи данных оценки от ученика инструктору на сервер
 */
open class GradeByStudentToInstructorModel(
    var classId: Int,
    var grade: Byte,
    var comment: String?,
){
    var gradeEnum : GradesByStudentsToInstructors
        get() = GradesByStudentsToInstructors.ofValue(grade)
        set(value) {
            grade = value.value
        }
}

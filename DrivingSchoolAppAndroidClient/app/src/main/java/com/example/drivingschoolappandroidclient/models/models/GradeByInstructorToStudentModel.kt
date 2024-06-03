package com.example.drivingschoolappandroidclient.models.models

/**
Модель для передачи данных оценки от инструктора ученику на сервер
 */
open class GradeByInstructorToStudentModel(
    var classId: Int,
    var grade: Byte,
    var comment: String?,
){
    var gradeEnum : GradesByInstructorsToStudents
        get() = GradesByInstructorsToStudents.ofValue(grade)
        set(value) {
            grade = value.value
        }
}

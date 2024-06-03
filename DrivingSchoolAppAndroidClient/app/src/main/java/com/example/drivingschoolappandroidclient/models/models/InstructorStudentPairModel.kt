package com.example.drivingschoolappandroidclient.models.models

/**
 * Модель для передачи пары значений id ученика и инструктора на сервер
 */
data class InstructorStudentPairModel(
    val instructorId: Int,
    val studentId: Int,
)

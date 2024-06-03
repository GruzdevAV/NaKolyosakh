package com.example.drivingschoolappandroidclient.models.models

class StudentRating(
    studentId: Int,
    userId: String,
    user: ApplicationUser,
    var numberOfGrades: Int?,
    var grade: Float?
) : Student(studentId, userId, user) {
}
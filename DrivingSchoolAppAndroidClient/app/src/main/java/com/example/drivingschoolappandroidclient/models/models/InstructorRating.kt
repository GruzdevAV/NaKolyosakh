package com.example.drivingschoolappandroidclient.models.models

class InstructorRating(
    instructorId: Int,
    userId: String,
    user: ApplicationUser,
    var numberOfGrades: Int,
    var grade: Float
) : Instructor(instructorId, userId, user) {
}
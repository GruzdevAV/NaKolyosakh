package com.example.drivingschoolappandroidclient.models.api

import com.example.drivingschoolappandroidclient.models.models.AddOuterScheduleModel
import com.example.drivingschoolappandroidclient.models.models.Class
import com.example.drivingschoolappandroidclient.models.models.GradeByInstructorToStudent
import com.example.drivingschoolappandroidclient.models.models.GradeByInstructorToStudentModel
import com.example.drivingschoolappandroidclient.models.models.GradeByStudentToInstructor
import com.example.drivingschoolappandroidclient.models.models.InnerScheduleOfInstructor
import com.example.drivingschoolappandroidclient.models.models.MyResponse
import com.example.drivingschoolappandroidclient.models.models.Student
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST

interface InstructorApiModel {
    @POST("GetMyStudents")
    fun getMyStudents(
        @Body instructorUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<List<Student>?>>
    @POST("GetClassesOfInstructor")
    fun getClassesOfInstructor(
        @Body instructorUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<List<Class>?>>
    @POST("GetMyInnerSchedules")
    fun getMyInnerSchedules(
        @Body instructorUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<List<InnerScheduleOfInstructor>?>>
    @POST("PostGradeToStudentForClass")
    fun postGradeToStudentForClass(
        @Body model: GradeByInstructorToStudentModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<GradeByInstructorToStudent?>>
    @POST("GetGradesToInstructor")
    fun getGradesToInstructor(
        @Body instructorUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<List<GradeByStudentToInstructor>?>>
    @POST("GetGradesByInstructor")
    fun getGradesByInstructor(
        @Body instructorUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<List<GradeByInstructorToStudent>?>>
    @POST("AddOuterScheduleToMe")
    fun addOuterScheduleToMe(
        @Body model: AddOuterScheduleModel,
        @Header("Authorization") authHead: String
        // TODO
    ) : Call<MyResponse<Any?>>
}

package com.example.drivingschoolappandroidclient.models.api

import com.example.drivingschoolappandroidclient.models.models.Class
import com.example.drivingschoolappandroidclient.models.models.GradeByInstructorToStudent
import com.example.drivingschoolappandroidclient.models.models.GradeByStudentToInstructor
import com.example.drivingschoolappandroidclient.models.models.GradeByStudentToInstructorModel
import com.example.drivingschoolappandroidclient.models.models.InnerScheduleOfInstructor
import com.example.drivingschoolappandroidclient.models.models.Instructor
import com.example.drivingschoolappandroidclient.models.models.MyResponse
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST

interface StudentApiModel {
    @POST("GetMyInstructor")
    fun getMyInstructor(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Instructor?>>
    @POST("GetClassesOfStudent")
    fun getClassesOfStudent(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<List<Class>?>>
    @POST("GetClassesOfMyInstructor")
    fun getClassesOfMyInstructor(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<List<Class>?>>
    @POST("GetInnerSchedulesOfMyInstructor")
    fun getInnerSchedulesOfMyInstructor(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<List<InnerScheduleOfInstructor>?>>
    @POST("PostGradeToInstructorForClass")
    fun postGradeToInstructorForClass(
        @Body model: GradeByStudentToInstructorModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<GradeByStudentToInstructor?>>
    @POST("GetGradesToStudent")
    fun getGradesToStudent(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<List<GradeByInstructorToStudent>?>>
    @POST("GetGradesByStudent")
    fun getGradesByStudent(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<List<GradeByStudentToInstructor>?>>
}

package com.example.drivingschoolappandroidclient.models.api

import com.example.drivingschoolappandroidclient.models.models.ApplicationUser
import com.example.drivingschoolappandroidclient.models.models.InnerScheduleOfInstructorModel
import com.example.drivingschoolappandroidclient.models.models.Instructor
import com.example.drivingschoolappandroidclient.models.models.InstructorScheduleModel
import com.example.drivingschoolappandroidclient.models.models.InstructorStudentPairModel
import com.example.drivingschoolappandroidclient.models.models.MyResponse
import com.example.drivingschoolappandroidclient.models.models.RegisterModel
import com.example.drivingschoolappandroidclient.models.models.Student
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST

interface AdminApiModel {
    @GET("GetUsers")
    fun getUsers(@Header("Authorization") authHead: String) :
            Call<MyResponse<List<ApplicationUser>?>>
    @POST("SetInstructorToStudent")
    fun setInstructorToStudent(
        @Body model: InstructorStudentPairModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<InstructorStudentPairModel?>>
    @POST("RegisterStudent")
    fun registerStudent(
        @Body model: RegisterModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Student?>>
    @POST("RegisterInstructor")
    fun registerInstructor(
        @Body model: RegisterModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Instructor?>>
    @POST("SetInnerSchedule")
    fun setInnerSchedule(
        @Body model: InstructorScheduleModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<InnerScheduleOfInstructorModel?>>

}
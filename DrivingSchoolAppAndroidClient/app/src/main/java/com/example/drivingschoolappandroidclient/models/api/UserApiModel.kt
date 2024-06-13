package com.example.drivingschoolappandroidclient.models.api

import com.example.drivingschoolappandroidclient.models.models.ApplicationUser
import com.example.drivingschoolappandroidclient.models.models.Class
import com.example.drivingschoolappandroidclient.models.models.EditMeModel
import com.example.drivingschoolappandroidclient.models.models.InnerScheduleOfInstructor
import com.example.drivingschoolappandroidclient.models.models.Instructor
import com.example.drivingschoolappandroidclient.models.models.InstructorRating
import com.example.drivingschoolappandroidclient.models.models.MyResponse
import com.example.drivingschoolappandroidclient.models.models.SetMeImageModel
import com.example.drivingschoolappandroidclient.models.models.Student
import com.example.drivingschoolappandroidclient.models.models.StudentRating
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST
import retrofit2.http.Query

interface UserApiModel {
    @GET("GetInstructors")
    fun getInstructors(@Header("Authorization") authHead: String) :
            Call<MyResponse<List<Instructor>?>>
    @GET("GetStudents")
    fun getStudents(@Header("Authorization") authHead: String) : Call<MyResponse<List<Student>?>>
    @GET("GetInstructor")
    fun getInstructor(
        @Query("instructorId") instructorId: Int,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Instructor?>>
    @GET("GetStudent")
    fun getStudent(
        @Query("studentId") studentId: Int,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Student?>>
    @GET("GetClass")
    fun getClass(
        @Query("classId") classId: Int,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Class?>>
    @GET("GetClasses")
    fun getClasses(@Header("Authorization") authHead: String) : Call<MyResponse<List<Class>?>>
    @GET("GetInnerSchedules")
    fun getInnerSchedules(@Header("Authorization") authHead: String) :
            Call<MyResponse<List<InnerScheduleOfInstructor>?>>
    @GET("GetInstructorRatings")
    fun getInstructorRatings(@Header("Authorization") authHead: String) :
            Call<MyResponse<List<InstructorRating>?>>
    @POST("GetStudentRatings")
    fun getStudentRatings(@Header("Authorization") authHead: String) :
            Call<MyResponse<List<StudentRating>?>>
    @GET("GetStudentRating")
    fun getStudentRating(
        @Query("studentId") studentId: Int,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<StudentRating?>>
    @GET("GetInstructorRating")
    fun getInstructorRating(
        @Query("instructorId") instructorId: Int,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<InstructorRating?>>
    @POST("GetStudentRatings")
    fun getMyStudentRatings(
        @Body instructorId: String,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<List<StudentRating>?>>
    @POST("GetMe")
    fun getMe(@Body userId: String,@Header("Authorization") authHead: String) :
            Call<MyResponse<ApplicationUser?>>
    @POST("EditMe")
    fun editMe(
        @Body model: EditMeModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<ApplicationUser?>>
    @POST("SetMeImage")
    fun setMeImage(
        @Body model: SetMeImageModel,
        @Header("Authorization") authHead: String
        // TODO
    ): Call<MyResponse<Any?>>



}

package com.example.drivingschoolappandroidclient.models.api

import com.example.drivingschoolappandroidclient.models.models.ClassStudentPairModel
import com.example.drivingschoolappandroidclient.models.models.MyResponse
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST

interface InstructorOrStudentApiModel {
    @POST("SetClass")
    fun setClass(
        @Body model: ClassStudentPairModel,
        @Header("Authorization") authHead: String): Call<MyResponse<ClassStudentPairModel?>>
    @POST("CancelClass")
    fun cancelClass(
        @Body classId: Int,
        @Header("Authorization") authHead: String): Call<MyResponse<Int?>>
}

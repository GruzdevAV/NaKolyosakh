package com.example.drivingschoolappandroidclient

import okhttp3.OkHttpClient
import okhttp3.ResponseBody
import org.junit.Before
import org.junit.Test
import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.http.GET
import retrofit2.http.Query

/**
 * Example local unit test, which will execute on the development machine (host).
 *
 * See [testing documentation](http://d.android.com/tools/testing).
 */
class ExampleUnitTest {
    lateinit var api: TestApi
    @Before
    fun init(){
        val client = OkHttpClient().newBuilder()
            .followRedirects(true)
            .followSslRedirects(true)
            .build()
        val retrofit: Retrofit = Retrofit.Builder()
            .baseUrl("http://localhost:5078")
            .client(client)
            .build()
        api = retrofit.create(TestApi::class.java)
    }
    @Test
    fun test() {
        try{
            val t = api.getHtml(val2=1).execute()
            val w = t.body()
        }
        catch (e: Exception){
            val w = e.message
        }
    }
}

interface TestApi{
    @GET("/swagger/index.html")
    fun getHtml() : Call<ResponseBody>
    @GET("/swagger/index.html")
    fun getHtml(@Query("val1") val1: Int?=null,@Query("val2") val2: Int?=null) : Call<ResponseBody>
}
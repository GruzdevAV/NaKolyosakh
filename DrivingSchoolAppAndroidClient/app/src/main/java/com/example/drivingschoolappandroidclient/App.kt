package com.example.drivingschoolappandroidclient

import android.app.Application
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.Window
import android.view.WindowManager
import android.widget.FrameLayout
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.MutableLiveData
import com.example.drivingschoolappandroidclient.models.api.Controller
import com.example.drivingschoolappandroidclient.models.models.ApplicationUser
import com.example.drivingschoolappandroidclient.models.models.Class
import com.example.drivingschoolappandroidclient.models.models.GetClassCallback
import com.example.drivingschoolappandroidclient.models.models.GetClassesCallback
import com.example.drivingschoolappandroidclient.models.models.GetClassesOfMyInstructorCallback
import com.example.drivingschoolappandroidclient.models.models.GetGradesByInstructorsToStudentCallback
import com.example.drivingschoolappandroidclient.models.models.GetGradesByStudentsToInstructorCallback
import com.example.drivingschoolappandroidclient.models.models.GetInnerSchedulesCallback
import com.example.drivingschoolappandroidclient.models.models.GetInstructorCallback
import com.example.drivingschoolappandroidclient.models.models.GetInstructorRatingCallback
import com.example.drivingschoolappandroidclient.models.models.GetInstructorRatingsCallback
import com.example.drivingschoolappandroidclient.models.models.GetInstructorsCallback
import com.example.drivingschoolappandroidclient.models.models.GetMeCallback
import com.example.drivingschoolappandroidclient.models.models.GetMyClassesCallback
import com.example.drivingschoolappandroidclient.models.models.GetMyInstructorCallback
import com.example.drivingschoolappandroidclient.models.models.GetMySchedulesCallback
import com.example.drivingschoolappandroidclient.models.models.GetMyStudentRatingsCallBack
import com.example.drivingschoolappandroidclient.models.models.GetMyStudentsCallback
import com.example.drivingschoolappandroidclient.models.models.GetStudentCallback
import com.example.drivingschoolappandroidclient.models.models.GetStudentRatingCallback
import com.example.drivingschoolappandroidclient.models.models.GetStudentRatingsCallBack
import com.example.drivingschoolappandroidclient.models.models.GetStudentsCallback
import com.example.drivingschoolappandroidclient.models.models.GradeByInstructorToStudent
import com.example.drivingschoolappandroidclient.models.models.GradeByStudentToInstructor
import com.example.drivingschoolappandroidclient.models.models.InnerScheduleOfInstructor
import com.example.drivingschoolappandroidclient.models.models.Instructor
import com.example.drivingschoolappandroidclient.models.models.InstructorRating
import com.example.drivingschoolappandroidclient.models.models.LoginResponse
import com.example.drivingschoolappandroidclient.models.models.MyResponse
import com.example.drivingschoolappandroidclient.models.models.Student
import com.example.drivingschoolappandroidclient.models.models.StudentRating
import com.example.drivingschoolappandroidclient.models.models.UserRoles
import retrofit2.Response
import java.sql.Date
import java.time.LocalDate
import java.time.LocalTime
import java.time.format.DateTimeFormatter
import java.util.Calendar

class App : Application() {

    companion object{
        val dateFormat = DateTimeFormatter.ofPattern("dd.MM.yyyy")
        val timeFormat = DateTimeFormatter.ISO_LOCAL_TIME
        // Блокировка экрана
        var blocked = MutableLiveData(false)
        lateinit var controller: Controller
        var response = MutableLiveData<MyResponse<*>>(null)
        val userId get() = controller.loginResponse.value!!.id
        val authHead get() = controller.loginResponse.value!!.authHead
        val userRole get() = controller.loginResponse.value!!.role
        val loginToken get() = controller.loginResponse.value!!.token
        var students : MutableLiveData<MutableList<Student>?> = MutableLiveData(null)
        var studentRatings : MutableLiveData<MutableList<StudentRating>?> = MutableLiveData(null)
        var instructors : MutableLiveData<MutableList<Instructor>?> = MutableLiveData(null)
        var instructorRatings : MutableLiveData<MutableList<InstructorRating>?> = MutableLiveData(null)
        var classes : MutableLiveData<List<Class>?> = MutableLiveData(null)
        var myClasses : MutableLiveData<List<Class>?> = MutableLiveData(null)
        var classesOfMyInstructor : MutableLiveData<List<Class>?> = MutableLiveData(null)
        var schedules : MutableLiveData<List<InnerScheduleOfInstructor>?> = MutableLiveData(null)
        var mySchedules : MutableLiveData<List<InnerScheduleOfInstructor>?> = MutableLiveData(null)
        var gradesByInstructorsToStudent : MutableLiveData<List<GradeByInstructorToStudent>?> = MutableLiveData(null)
        var gradesByStudentsToInstructor : MutableLiveData<List<GradeByStudentToInstructor>?> = MutableLiveData(null)
        var myInstructor: MutableLiveData<Instructor?> = MutableLiveData(null)
        var myInstructorRating: MutableLiveData<InstructorRating?> = MutableLiveData(null)
        var myStudents: MutableLiveData<List<Student>?> = MutableLiveData(null)
        var myStudentRatings: MutableLiveData<List<StudentRating>?> = MutableLiveData(null)
        var me : MutableLiveData<ApplicationUser?> = MutableLiveData(null)

        fun loginSaveToPreferences(it: LoginResponse?, activity: AppCompatActivity) {
            val preferences = activity.getSharedPreferences("login", Context.MODE_PRIVATE)
            val editor = preferences.edit()
            if(it==null) {
                editor.remove("id")
                    .remove("role")
                    .remove("token")
                    .remove("expiration")
                    .commit()
                return
            }
            editor.putString("id", it.id)
                .putString("role", it.role)
                .putString("token", it.token)
                .putLong("expiration", it.expiration.time)
                .commit()
        }
        private var loadingView: View? = null
        fun showWaitingScreen(window: Window, layoutInflater: LayoutInflater) {
            loadingView = layoutInflater.inflate(R.layout.loading, null)
            window.setFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE, WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE)
            val rootView = window.decorView.findViewById<FrameLayout>(android.R.id.content)
            rootView.addView(loadingView)
        }
        fun hideWaitingScreen(window: Window) {
            window.clearFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE)
            val rootView = window.decorView.findViewById<FrameLayout>(android.R.id.content)
            rootView.removeView(loadingView)
            loadingView = null
        }
        fun timeFromString(time: String) : LocalTime {
            val str = if(time.indexOfFirst { it==':' }<2)
                "0$time"
            else time
            return LocalTime.parse(str,timeFormat)
        }
        fun dateFromString(date: String) = LocalDate.parse(date, dateFormat)
        fun dateToString(date: LocalDate) = dateFormat.format(date)
        fun timeToString(time: LocalTime) = timeFormat.format(time)

        fun reloadStudents() {
            with(controller){
                api.getStudents(authHead).enqueue(GetStudentsCallback())
                api.getStudentRatings(authHead).enqueue(GetStudentRatingsCallBack())
                if(userRole == UserRoles.instructor){
                    api.getMyStudents(userId, authHead).enqueue(GetMyStudentsCallback())
                    api.getMyStudentRatings(userId, authHead).enqueue(
                        GetMyStudentRatingsCallBack()
                    )
                }
            }
        }

        fun reloadStudent(studentId: Int){
            controller.api.getStudentRating(studentId, authHead).enqueue(GetStudentRatingCallback())
            controller.api.getStudent(studentId, authHead).enqueue(GetStudentCallback())
        }

        fun reloadInstructors() {
            with(controller){
                api.getInstructors(authHead).enqueue(GetInstructorsCallback())
                api.getInstructorRatings(authHead).enqueue(GetInstructorRatingsCallback())
                if(userRole == UserRoles.student){
                    api.getMyInstructor(userId,authHead).enqueue(GetMyInstructorCallback())
                }
            }
        }
        fun reloadMe() = controller.api.getMe(userId, authHead).enqueue(GetMeCallback())

        fun reloadClasses() {
            controller.api.getClasses(authHead).enqueue(GetClassesCallback())
            reloadMyClasses()
        }

        private fun reloadMyClasses() {
            when (userRole){
                UserRoles.student -> {
                    controller.api.getClassesOfStudent(userId, authHead)
                        .enqueue(GetMyClassesCallback())
                    controller.api.getClassesOfMyInstructor(userId, authHead)
                        .enqueue(GetClassesOfMyInstructorCallback())
                }
                UserRoles.instructor ->
                    controller.api.getClassesOfInstructor(userId, authHead)
                        .enqueue(GetMyClassesCallback())
            }
        }

        fun reloadInnerSchedules() {
            controller.api.getInnerSchedules(authHead).enqueue(GetInnerSchedulesCallback())
            reloadMyInnerSchedules()
        }
        fun reloadMyInnerSchedules() {
            when (userRole){
                UserRoles.student ->
                    controller.api.getInnerSchedulesOfMyInstructor(userId, authHead)
                        .enqueue(GetMySchedulesCallback())
                UserRoles.instructor -> controller.api.getMyInnerSchedules(userId, authHead)
                    .enqueue(GetMySchedulesCallback())
            }
        }

        fun unblock() {
            blocked.value = false
        }
        fun block() {
            blocked.value = true
        }

        fun onFailure(p1: Throwable) {
            p1.printStackTrace()
            response.value = MyResponse(
                status = "Возникла ошибка",
                message = p1.message?:"",
                `package` = null
            )
            unblock()
        }

        fun <T> onFailure(p1: Response<T>)
                where T : MyResponse<*>? {
            response.value = MyResponse(
                status = "Возникла ошибка",
                message = p1.message()?:"",
                `package` = null
            )
            unblock()
        }

        fun getLoginResponse(activity: AppCompatActivity): LoginResponse? {
            val preferences = activity.getSharedPreferences("login", Context.MODE_PRIVATE)
            val token = preferences.getString("token",null)
            val expiration = Date(preferences.getLong("expiration",-1L))
            val id = preferences.getString("id",null)
            val role = preferences.getString("role",null)
            if(token==null||id==null||role==null||expiration.time < Calendar.getInstance().time.time)
                return null
            return LoginResponse(token,
                expiration,
                role,
                id,
            )
        }

        fun reloadGrades() {
            when (userRole){
                UserRoles.student -> {
                    controller.api.getGradesToStudent(userId, authHead)
                        .enqueue(GetGradesByInstructorsToStudentCallback())
                    controller.api.getGradesByStudent(userId, authHead)
                        .enqueue(GetGradesByStudentsToInstructorCallback())
                }
                UserRoles.instructor -> {
                    controller.api.getGradesByInstructor(userId, authHead)
                        .enqueue(GetGradesByInstructorsToStudentCallback())
                    controller.api.getGradesToInstructor(userId, authHead)
                        .enqueue(GetGradesByStudentsToInstructorCallback())
                }
            }
        }

        fun reloadClass(classId: Int) {
            controller.api.getClass(classId, authHead).enqueue(GetClassCallback())
        }

        fun reloadInstructor(instructorId: Int) {
            controller.api.getInstructorRating(instructorId, authHead).enqueue(
                GetInstructorRatingCallback()
            )
            controller.api.getInstructor(instructorId, authHead).enqueue(GetInstructorCallback())
        }
    }

    override fun onCreate() {
        super.onCreate()
        controller = Controller()
        controller.start()
    }
}




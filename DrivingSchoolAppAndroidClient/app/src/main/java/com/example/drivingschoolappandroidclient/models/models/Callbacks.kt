package com.example.drivingschoolappandroidclient.models.models

import android.util.Log
import com.example.drivingschoolappandroidclient.App
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class GetMeCallback: MyCallback<MyResponse<ApplicationUser?>>() {
    override fun onResponse(
        p0: Call<MyResponse<ApplicationUser?>>,
        p1: Response<MyResponse<ApplicationUser?>>
    ) {
        Log.d("GetMe","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.me.value = p1.body()!!.`package`
        App.blocked.value = false
    }
}
class GetClassesCallback: MyCallback<MyResponse<List<Class>?>>() {

    override fun onResponse(p0: Call<MyResponse<List<Class>?>>, p1: Response<MyResponse<List<Class>?>>) {
        Log.d("GetClasses","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.classes.value = p1.body()!!.`package`
        App.blocked.value = false
    }

}
class GetInnerSchedulesCallback :
    MyCallback<MyResponse<List<InnerScheduleOfInstructor>?>>() {
    override fun onResponse(
        p0: Call<MyResponse<List<InnerScheduleOfInstructor>?>>,
        p1: Response<MyResponse<List<InnerScheduleOfInstructor>?>>
    ) {
        Log.d("GetInnerSchedules","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.schedules.value = p1.body()!!.`package`
        App.blocked.value = false
    }
}
class GetMyInstructorCallback :
    MyCallback<MyResponse<Instructor?>>() {
    override fun onResponse(
        p0: Call<MyResponse<Instructor?>>,
        p1: Response<MyResponse<Instructor?>>
    ) {
        Log.d("GetMyInstructor","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.myInstructor.value = p1.body()!!.`package`
        App.blocked.value = false
    }



}
class GetInstructorRatingsCallback :
    MyCallback<MyResponse<List<InstructorRating>?>>() {
    override fun onResponse(
        p0: Call<MyResponse<List<InstructorRating>?>>,
        p1: Response<MyResponse<List<InstructorRating>?>>
    ) {
        Log.d("GetInstructorRatings","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.instructorRatings.value = p1.body()!!.`package`
        if (App.controller.loginResponse.value?.role == UserRoles.student)
            App.myInstructorRating.value = App.instructorRatings.value?.find {
                it.instructorId == App.myInstructor.value?.instructorId
            }
        App.blocked.value = false
    }
}
class GetInstructorsCallback :
    MyCallback<MyResponse<List<Instructor>?>>() {
    override fun onResponse(
        p0: Call<MyResponse<List<Instructor>?>>,
        p1: Response<MyResponse<List<Instructor>?>>
    ) {
        Log.d("GetInstructors","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.instructors.value = p1.body()!!.`package`
        App.blocked.value = false
    }
}
class GetMyStudentRatingsCallBack :
    MyCallback<MyResponse<List<StudentRating>?>>() {
    override fun onResponse(
        p0: Call<MyResponse<List<StudentRating>?>>,
        p1: Response<MyResponse<List<StudentRating>?>>
    ) {
        Log.d("GetMyStudentRatings","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.myStudentRatings.value = p1.body()!!.`package`
        App.blocked.value = false
    }
}
class GetMyStudentsCallback :
    MyCallback<MyResponse<List<Student>?>>() {
    override fun onResponse(
        p0: Call<MyResponse<List<Student>?>>,
        p1: Response<MyResponse<List<Student>?>>
    ) {
        Log.d("GetMyStudents","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.myStudents.value = p1.body()!!.`package`
        App.blocked.value = false
    }
}
class GetStudentRatingsCallBack :
    MyCallback<MyResponse<List<StudentRating>?>>() {
    override fun onResponse(
        p0: Call<MyResponse<List<StudentRating>?>>,
        p1: Response<MyResponse<List<StudentRating>?>>
    ) {
        Log.d("GetStudentRatings","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.studentRatings.value = p1.body()!!.`package`
        App.blocked.value = false
    }
}
class EditMeCallback : MyCallback<MyResponse<ApplicationUser?>>() {
    override fun onResponse(
        p0: Call<MyResponse<ApplicationUser?>>,
        p1: Response<MyResponse<ApplicationUser?>>
    ) {
        Log.d("EditMe","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.blocked.value = false
    }
}
class GetStudentsCallback :
    MyCallback<MyResponse<List<Student>?>>() {
    override fun onResponse(p0: Call<MyResponse<List<Student>?>>, p1: Response<MyResponse<List<Student>?>>) {
        Log.d("GetStudents", "$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.students.value = p1.body()!!.`package`
        App.blocked.value = false
    }
}
class SetInstructorToStudentCallback :
    MyCallback<MyResponse<InstructorStudentPairModel?>>() {
    override fun onResponse(
        p0: Call<MyResponse<InstructorStudentPairModel?>>,
        p1: Response<MyResponse<InstructorStudentPairModel?>>
    ) {
        Log.d("SetInstructorToStudent","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.response.value = p1.body()
        App.blocked.value = false
        App.reloadStudents()
    }
}
class AddOuterScheduleCallback :
    MyCallback<MyResponse<Any?>>() {
    override fun onResponse(
        p0: Call<MyResponse<Any?>>,
        p1: Response<MyResponse<Any?>>
    ) {
        Log.d("AddOuterSchedule","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.reloadInnerSchedules()
        App.reloadMyInnerSchedules()
        App.blocked.value = false
    }
}
class SetInnerScheduleCallback :
    MyCallback<MyResponse<InnerScheduleOfInstructorModel?>>() {
    override fun onResponse(
        p0: Call<MyResponse<InnerScheduleOfInstructorModel?>>,
        p1: Response<MyResponse<InnerScheduleOfInstructorModel?>>
    ) {
        Log.d("SetInnerSchedule","$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.reloadInnerSchedules()
        App.reloadClasses()
        App.blocked.value = false
    }
}
class GetMySchedulesCallback :
    MyCallback<MyResponse<List<InnerScheduleOfInstructor>?>>() {
    override fun onResponse(
        p0: Call<MyResponse<List<InnerScheduleOfInstructor>?>>,
        p1: Response<MyResponse<List<InnerScheduleOfInstructor>?>>
    ) {
        Log.d("GetMySchedules", "$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.mySchedules.value = p1.body()!!.`package`
        App.blocked.value = false
    }


}
class GetMyClassesCallback :
    MyCallback<MyResponse<List<Class>?>>() {
    override fun onResponse(
        p0: Call<MyResponse<List<Class>?>>,
        p1: Response<MyResponse<List<Class>?>>
    ) {
        Log.d("GetMyClasses", "$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.myClasses.value = p1.body()!!.`package`
        App.blocked.value = false
    }
}
class GetGradesByInstructorsToStudentCallback :
    MyCallback<MyResponse<List<GradeByInstructorToStudent>?>>() {
    override fun onResponse(
        p0: Call<MyResponse<List<GradeByInstructorToStudent>?>>,
        p1: Response<MyResponse<List<GradeByInstructorToStudent>?>>
    ) {
        Log.d("GetGradesByInstructorsToStudent", "$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.gradesByInstructorsToStudent.value = p1.body()!!.`package`
        App.blocked.value = false
    }



}
class GetGradesByStudentsToInstructorCallback :
    MyCallback<MyResponse<List<GradeByStudentToInstructor>?>>() {
    override fun onResponse(
        p0: Call<MyResponse<List<GradeByStudentToInstructor>?>>,
        p1: Response<MyResponse<List<GradeByStudentToInstructor>?>>
    ) {
        Log.d("GetGradesByStudentsToInstructor", "$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.gradesByStudentsToInstructor.value = p1.body()!!.`package`
        App.blocked.value = false
    }
}
abstract class MyCallback<T> : Callback<T>
    where T : MyResponse<*>{
    // Вызывается в случае ошибки
    override fun onResponse(p0: Call<T>, p1: Response<T>) = App.onFailure(p1)
    override fun onFailure(p0: Call<T>, p1: Throwable) = App.onFailure(p1)
}
class SetClassCallback : MyCallback<MyResponse<ClassStudentPairModel?>>() {
    override fun onResponse(
        p0: Call<MyResponse<ClassStudentPairModel?>>,
        p1: Response<MyResponse<ClassStudentPairModel?>>
    ) {
        Log.d("SetClass", "$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        App.reloadClasses()
        App.blocked.value = false
    }

}
class CancelClassCallback : MyCallback<MyResponse<Int?>>() {
    override fun onResponse(p0: Call<MyResponse<Int?>>, p1: Response<MyResponse<Int?>>) {
        Log.d("CancelClass", "$p1")
        if (p1.body() == null)
            return super.onResponse(p0, p1)
        p1.body()!!.`package`?.let{ App.reloadClass(it) }
        App.blocked.value = false
    }
}

class GetClassCallback : MyCallback<MyResponse<Class?>>() {
    override fun onResponse(p0: Call<MyResponse<Class?>>, p1: Response<MyResponse<Class?>>) {
        Log.d("GetClass", "$p1")
        if (p1.body()==null) return super.onResponse(p0, p1)
        p1.body()!!.`package`?.let { pIt ->
            var index = App.classes.value.orEmpty().indexOfFirst {
                it.classId == pIt.classId
            }
            if(index<0) (App.classes.value.orEmpty() as MutableList).add(pIt)
            else (App.classes.value!! as MutableList)[index] = pIt
            index = App.myClasses.value.orEmpty().indexOfFirst {
                it.classId == pIt.classId
            }
            if (index > -1) (App.myClasses.value!! as MutableList)[index] = pIt
        }
        App.blocked.value = false
    }
}

class PostGradeToStudentCallback : MyCallback<MyResponse<GradeByInstructorToStudent?>>() {
    override fun onResponse(p0: Call<MyResponse<GradeByInstructorToStudent?>>, p1: Response<MyResponse<GradeByInstructorToStudent?>>) {
        Log.d("PostGradeToStudent", "$p1")
        if (p1.body()==null) return super.onResponse(p0, p1)
        p1.body()?.`package`?.classId?.let{
            App.reloadClass(it)
            App.reloadGrades()
        }
        App.blocked.value = false
    }
}

class PostGradeToInstructorCallback : MyCallback<MyResponse<GradeByStudentToInstructor?>>() {
    override fun onResponse(p0: Call<MyResponse<GradeByStudentToInstructor?>>, p1: Response<MyResponse<GradeByStudentToInstructor?>>) {
        Log.d("PostGradeToInstructor", "$p1")
        if (p1.body()==null) return super.onResponse(p0, p1)
        p1.body()?.`package`?.classId?.let{
            App.reloadClass(it)
            App.reloadGrades()
        }
        App.blocked.value = false
    }
}
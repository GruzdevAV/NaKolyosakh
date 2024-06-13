package com.example.drivingschoolappandroidclient.models.models

import android.util.Log
import com.example.drivingschoolappandroidclient.App
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

abstract class MyCallback<T> : Callback<T>
        where T : MyResponse<*>{

    fun default(p0: Call<T>, p1: Response<T>, tag: String = "TAG", func: (it: T) -> Unit = {}){
        Log.d(tag,"$p1")
        p1.body()?.let{
            App.response.value = it
            func(it)
            App.unblock()
        } ?: return App.onFailure(p1)
    }
    // Вызываются в случае ошибки
    override fun onResponse(p0: Call<T>, p1: Response<T>) = App.onFailure(p1)
    override fun onFailure(p0: Call<T>, p1: Throwable) = App.onFailure(p1)

}

class GetMeCallback: MyCallback<MyResponse<ApplicationUser?>>() {

    override fun onResponse(
        p0: Call<MyResponse<ApplicationUser?>>,
        p1: Response<MyResponse<ApplicationUser?>>
    ) {
        default(p0,p1,"GetMe"){
            App.me.value = it.`package`
        }
//        Log.d("GetMe","$p1")
//        p1.body()?.let{
//            App.unblock()
//        } ?: return super.onResponse(p0, p1)
    }

}
class GetClassesCallback: MyCallback<MyResponse<List<Class>?>>() {

    override fun onResponse(p0: Call<MyResponse<List<Class>?>>, p1: Response<MyResponse<List<Class>?>>) {
        default(p0,p1,"GetClasses"){
            App.classes.value = it.`package`
        }
    }

}
class GetInnerSchedulesCallback :
    MyCallback<MyResponse<List<InnerScheduleOfInstructor>?>>() {

    override fun onResponse(
        p0: Call<MyResponse<List<InnerScheduleOfInstructor>?>>,
        p1: Response<MyResponse<List<InnerScheduleOfInstructor>?>>
    ) {
        default(p0,p1,"GetInnerSchedules"){
            App.schedules.value = it.`package`
        }
    }

}

class GetMyInstructorCallback :
    MyCallback<MyResponse<Instructor?>>() {

    override fun onResponse(
        p0: Call<MyResponse<Instructor?>>,
        p1: Response<MyResponse<Instructor?>>
    ) {
        default(p0,p1,"GetMyInstructor"){
            App.myInstructor.value = it.`package`
            App.myInstructorRating.value = App.instructorRatings.value?.first { ir ->
                ir.instructorId == App.myInstructor.value?.instructorId
            }
        }
    }

}

class GetInstructorRatingsCallback :
    MyCallback<MyResponse<List<InstructorRating>?>>() {

    override fun onResponse(
        p0: Call<MyResponse<List<InstructorRating>?>>,
        p1: Response<MyResponse<List<InstructorRating>?>>
    ) {
        default(p0,p1,"GetInstructorRatings"){
            App.instructorRatings.value = it.`package`?.toMutableList() ?: mutableListOf()
            if (App.controller.loginResponse.value?.role == UserRoles.student)
                App.myInstructorRating.value = App.instructorRatings.value?.find { ir ->
                    ir.instructorId == App.myInstructor.value?.instructorId
                }
        }
    }

}

class GetInstructorsCallback :
    MyCallback<MyResponse<List<Instructor>?>>() {

    override fun onResponse(
        p0: Call<MyResponse<List<Instructor>?>>,
        p1: Response<MyResponse<List<Instructor>?>>
    ) {
        default(p0,p1,"GetInstructors"){
            App.instructors.value = it.`package`?.toMutableList() ?: mutableListOf()
        }
    }

}

class GetMyStudentRatingsCallBack :
    MyCallback<MyResponse<List<StudentRating>?>>() {

    override fun onResponse(
        p0: Call<MyResponse<List<StudentRating>?>>,
        p1: Response<MyResponse<List<StudentRating>?>>
    ) {
        default(p0,p1,"GetMyStudentRatings"){
            App.myStudentRatings.value = it.`package`
        }
    }

}

class GetMyStudentsCallback :
    MyCallback<MyResponse<List<Student>?>>() {

    override fun onResponse(
        p0: Call<MyResponse<List<Student>?>>,
        p1: Response<MyResponse<List<Student>?>>
    ) {
        default(p0,p1,"GetMyStudents"){
            App.myStudents.value = it.`package`
        }
    }

}

class GetStudentRatingsCallBack :
    MyCallback<MyResponse<List<StudentRating>?>>() {

    override fun onResponse(
        p0: Call<MyResponse<List<StudentRating>?>>,
        p1: Response<MyResponse<List<StudentRating>?>>
    ) {
        default(p0,p1,"GetStudentRatings"){
            App.studentRatings.value = it.`package`?.toMutableList()
        }
    }

}

class EditMeCallback : MyCallback<MyResponse<ApplicationUser?>>() {

    override fun onResponse(
        p0: Call<MyResponse<ApplicationUser?>>,
        p1: Response<MyResponse<ApplicationUser?>>
    ) {
        default(p0,p1,"EditMe")
    }

}

class GetStudentsCallback :
    MyCallback<MyResponse<List<Student>?>>() {

    override fun onResponse(
        p0: Call<MyResponse<List<Student>?>>,
        p1: Response<MyResponse<List<Student>?>>
    ) {
        default(p0,p1,"GetStudents"){
            App.students.value = it.`package`?.toMutableList()
        }
    }

}

class SetInstructorToStudentCallback :
    MyCallback<MyResponse<InstructorStudentPairModel?>>() {

    override fun onResponse(
        p0: Call<MyResponse<InstructorStudentPairModel?>>,
        p1: Response<MyResponse<InstructorStudentPairModel?>>
    ) {
        default(p0, p1, "SetInstructorToStudent") {
            it.`package`?.let { model ->
                App.reloadStudent(model.studentId)
            }
        }
    }

}

class GetStudentCallback : MyCallback<MyResponse<Student?>>() {

    override fun onResponse(
        p0: Call<MyResponse<Student?>>,
        p1: Response<MyResponse<Student?>>
    ) {
        default(p0,p1,"GetStudent"){
            it.`package`?.let{  `package` ->
                App.students.value?.let { students ->
                    val index = students.indexOfFirst { student ->
                        student.studentId == `package`.studentId
                    }
                    if (index > -1) students[index] = `package`
                    else students.add(`package`)
                }
            }
        }
    }

}


class GetStudentRatingCallback : MyCallback<MyResponse<StudentRating?>>() {

    override fun onResponse(
        p0: Call<MyResponse<StudentRating?>>,
        p1: Response<MyResponse<StudentRating?>>
    ) {
        default(p0,p1,"GetStudentRating"){
            it.`package`?.let{  `package` ->
                App.studentRatings.value?.let { studentRatings ->
                    val index = studentRatings.indexOfFirst { studentRating ->
                        studentRating.studentId == `package`.studentId
                    }
                    if (index > -1) studentRatings[index] = `package`
                    else studentRatings.add(`package`)
                }
            }
        }
    }

}

class AddOuterScheduleCallback :
    MyCallback<MyResponse<Any?>>() {

    override fun onResponse(
        p0: Call<MyResponse<Any?>>,
        p1: Response<MyResponse<Any?>>
    ) {
        default(p0,p1,"AddOuterSchedule"){
            App.reloadInnerSchedules()
            App.reloadMyInnerSchedules()
        }
    }

}

class SetInnerScheduleCallback :
    MyCallback<MyResponse<InnerScheduleOfInstructorModel?>>() {

    override fun onResponse(
        p0: Call<MyResponse<InnerScheduleOfInstructorModel?>>,
        p1: Response<MyResponse<InnerScheduleOfInstructorModel?>>
    ) {
        default(p0,p1,"SetInnerSchedule"){
            App.reloadInnerSchedules()
            App.reloadClasses()
        }
    }

}

class GetMySchedulesCallback :
    MyCallback<MyResponse<List<InnerScheduleOfInstructor>?>>() {
    override fun onResponse(
        p0: Call<MyResponse<List<InnerScheduleOfInstructor>?>>,
        p1: Response<MyResponse<List<InnerScheduleOfInstructor>?>>
    ) {
        default(p0,p1,"GetMySchedules"){
            App.mySchedules.value = it.`package`
        }
    }

}

class GetMyClassesCallback :
    MyCallback<MyResponse<List<Class>?>>() {

    override fun onResponse(
        p0: Call<MyResponse<List<Class>?>>,
        p1: Response<MyResponse<List<Class>?>>
    ) {
        default(p0,p1,"GetMyClasses"){
            App.myClasses.value = it.`package`
        }
    }

}

class GetClassesOfMyInstructorCallback :
    MyCallback<MyResponse<List<Class>?>>() {

    override fun onResponse(
        p0: Call<MyResponse<List<Class>?>>,
        p1: Response<MyResponse<List<Class>?>>
    ) {
        default(p0,p1,"GetMyClassesGetClassesOfMyInstructor"){
            App.classesOfMyInstructor.value = it.`package`
        }
    }

}

class GetGradesByInstructorsToStudentCallback :
    MyCallback<MyResponse<List<GradeByInstructorToStudent>?>>() {

    override fun onResponse(
        p0: Call<MyResponse<List<GradeByInstructorToStudent>?>>,
        p1: Response<MyResponse<List<GradeByInstructorToStudent>?>>
    ) {
        default(p0,p1,"GetGradesByInstructorsToStudent"){
            App.gradesByInstructorsToStudent.value = it.`package`
        }
    }

}

class GetGradesByStudentsToInstructorCallback :
    MyCallback<MyResponse<List<GradeByStudentToInstructor>?>>() {

    override fun onResponse(
        p0: Call<MyResponse<List<GradeByStudentToInstructor>?>>,
        p1: Response<MyResponse<List<GradeByStudentToInstructor>?>>
    ) {
        default(p0,p1,"GetGradesByStudentsToInstructor"){
            App.gradesByStudentsToInstructor.value = it.`package`
        }
    }

}

class SetClassCallback : MyCallback<MyResponse<ClassStudentPairModel?>>() {

    override fun onResponse(
        p0: Call<MyResponse<ClassStudentPairModel?>>,
        p1: Response<MyResponse<ClassStudentPairModel?>>
    ) {
        default(p0,p1,"SetClass"){
            it.`package`?.let{ model ->
                App.reloadClass(model.classId)
            }
        }
    }

}

class CancelClassCallback : MyCallback<MyResponse<Int?>>() {

    override fun onResponse(p0: Call<MyResponse<Int?>>, p1: Response<MyResponse<Int?>>) {
        default(p0,p1,"CancelClass"){
            it.`package`?.let{ classId ->
                App.reloadClass(classId)
            }
        }
    }

}

class GetClassCallback : MyCallback<MyResponse<Class?>>() {

    override fun onResponse(p0: Call<MyResponse<Class?>>, p1: Response<MyResponse<Class?>>) {
        default(p0,p1,"GetClass"){ response ->
            response.`package`?.let { pIt ->
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
        }
    }

}

class PostGradeToStudentCallback : MyCallback<MyResponse<GradeByInstructorToStudent?>>() {

    override fun onResponse(
        p0: Call<MyResponse<GradeByInstructorToStudent?>>,
        p1: Response<MyResponse<GradeByInstructorToStudent?>>
    ) {
        default(p0,p1,"PostGradeToStudent"){
            it.`package`?.classId?.let{classId ->
                App.reloadClass(classId)
                App.reloadGrades()
            }
        }
    }

}

class PostGradeToInstructorCallback : MyCallback<MyResponse<GradeByStudentToInstructor?>>() {

    override fun onResponse(
        p0: Call<MyResponse<GradeByStudentToInstructor?>>,
        p1: Response<MyResponse<GradeByStudentToInstructor?>>
    ) {
        default(p0,p1,"PostGradeToInstructor"){
            it.`package`?.classId?.let{ classId ->
                App.reloadClass(classId)
                App.reloadGrades()
            }
        }
    }

}

class RegisterStudentCallback :
    MyCallback<MyResponse<Student?>>() {

    override fun onResponse(p0: Call<MyResponse<Student?>>, p1: Response<MyResponse<Student?>>) {
        default(p0, p1, "RegisterStudent"){ response ->
            response.`package`?.let {
                App.reloadStudent(it.studentId)
            }
        }
    }

}

class RegisterInstructorCallback : MyCallback<MyResponse<Instructor?>>() {

    override fun onResponse(p0: Call<MyResponse<Instructor?>>, p1: Response<MyResponse<Instructor?>>) {
        default(p0, p1, "RegisterInstructor"){ response ->
            response.`package`?.let {
                App.reloadInstructor(it.instructorId)
            }
        }
    }

}

class GetInstructorRatingCallback : MyCallback<MyResponse<InstructorRating?>>() {

    override fun onResponse(
        p0: Call<MyResponse<InstructorRating?>>,
        p1: Response<MyResponse<InstructorRating?>>
    ) {
        default(p0,p1,"GetInstructorRating"){
            it.`package`?.let{  `package` ->
                App.instructorRatings.value?.let { instructorRatings ->
                    val index = instructorRatings.indexOfFirst { instructorRating ->
                        instructorRating.instructorId == `package`.instructorId
                    }
                    if (index > -1) instructorRatings[index] = `package`
                    else instructorRatings.add(`package`)
                }
            }
        }
    }

}

class GetInstructorCallback :
    MyCallback<MyResponse<Instructor?>>() {

    override fun onResponse(
        p0: Call<MyResponse<Instructor?>>,
        p1: Response<MyResponse<Instructor?>>
    ) {
        default(p0,p1,"GetInstructor"){
            it.`package`?.let{  `package` ->
                App.instructors.value?.let { instructors ->
                    val index = instructors.indexOfFirst { instructor ->
                        instructor.instructorId == `package`.instructorId
                    }
                    if (index > -1) instructors[index] = `package`
                    else instructors.add(`package`)
                }
            }
        }
    }
}



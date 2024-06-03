package com.example.drivingschoolappandroidclient.ui.students

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.models.models.StudentRating
import com.example.drivingschoolappandroidclient.models.models.UserRoles
import com.example.drivingschoolappandroidclient.databinding.PersonItemBinding

class StudentAdapter : RecyclerView.Adapter<StudentAdapter.StudentViewHolder>() {

    val students : List<StudentRating>? get(){
        return if(mShowMyStudents)
            App.myStudentRatings.value
        else App.studentRatings.value
    }
    class StudentViewHolder(val binding: PersonItemBinding): RecyclerView.ViewHolder(binding.root)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): StudentViewHolder =
        StudentViewHolder(PersonItemBinding.inflate(LayoutInflater.from(parent.context),parent,false))

    override fun getItemCount(): Int = students!!.size
    private var mShowMyStudents = false
    var showMyStudents: Boolean
        get() = mShowMyStudents
        set(value) {
            selectedPosition = null
            notifyDataSetChanged()
            mShowMyStudents = value
        }
    var selectedPosition : Int? = null
    val selected get() = selectedPosition?.let{students!![it]}
    override fun onBindViewHolder(holder: StudentViewHolder, position: Int) {
        val element = students!![position]
        with(holder.binding){

            if(App.controller.loginResponse.value!!.role != UserRoles.admin)
                tvSel.visibility = View.GONE
            else {
                root.setOnClickListener {
                    tvSel.visibility = View.VISIBLE
                    selectedPosition?.let { it1 -> this@StudentAdapter.notifyItemChanged(it1) }
                    notifyItemChanged(position)
                    if (selectedPosition == position){
                        selectedPosition = null
                        return@setOnClickListener
                    }
                    selectedPosition = position
                }
                val sel =
                    if (selectedPosition == position) "●"
                    else "○"
                tvSel.text = sel
            }
            element.profilePicture?.let{ivItemProfile.setImageBitmap(it)}
            tvItemFirstname.text = element.firstName
            tvItemLastname.text = element.lastName
            tvItemPatronymic.text = element.patronymic?:""
            tvItemEmail.text = element.user.email
            tvItemPhone.text = element.user.phoneNumber
            tvItemGradesCount.text = (element.numberOfGrades ?: 0).toString()
            tvItemAvgGrade.text = (element.grade ?: 0.0f).toString()
            val str = element.instructor?.let { "${it.lastName} ${it.firstName} ${it.patronymic?:""}"}?:"не назначен"
            tvItemInstructor.text = str
        }
    }
}
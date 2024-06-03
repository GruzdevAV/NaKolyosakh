package com.example.drivingschoolappandroidclient.ui.instructors

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.models.models.InstructorRating
import com.example.drivingschoolappandroidclient.databinding.PersonItemBinding

class InstructorAdapter : RecyclerView.Adapter<InstructorAdapter.InstructorViewHolder>() {

    val instructors : List<InstructorRating>
        get() {
            return if(showMyInstructor)
                App.myInstructorRating.value?.let{ listOf(it) } ?: listOf()
            else App.instructorRatings.value ?: listOf()
        }
    private var mShowMyInstructor: Boolean = false
    var showMyInstructor: Boolean
        get() = mShowMyInstructor
        set(value) {
            notifyDataSetChanged()
            mShowMyInstructor = value
        }

    class InstructorViewHolder(val binding: PersonItemBinding): RecyclerView.ViewHolder(binding.root)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int) = InstructorViewHolder(
        PersonItemBinding.inflate(LayoutInflater.from(parent.context),parent,false)
    )

    override fun getItemCount(): Int = instructors?.size ?: 0

    override fun onBindViewHolder(holder: InstructorViewHolder, position: Int) {
        val element = instructors!![position]
        with(holder.binding){
            tvSel.visibility = View.GONE
            element.profilePicture?.let{ivItemProfile.setImageBitmap(it)}
            tvItemFirstname.text = element.firstName
            tvItemLastname.text = element.lastName
            tvItemPatronymic.text = element.patronymic?:""
            tvItemEmail.text = element.user.email
            tvItemPhone.text = element.user.phoneNumber
            tvItemGradesCount.text = (element.numberOfGrades ?: 0).toString()
            tvItemAvgGrade.text = (element.grade ?: 0.0f).toString()
            trInstructorOfStudent.visibility = View.GONE
        }
    }
}
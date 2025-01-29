import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Grade, SubjectService } from '../subject.service';
import { NotificationService } from '../../../core/services/notification.service';
import { BaseResponse } from '../../../shared/models/base-model';

@Component({
  selector: 'app-subject-details',
  standalone: false,
  templateUrl: './subject-details.component.html',
  styleUrl: './subject-details.component.scss',
})
export class SubjectDetailsComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private subjectService = inject(SubjectService);
  private notificationService = inject(NotificationService);

  id = +(this.route.snapshot.paramMap.get('id') ?? 0);
  subjectWithStudents$ = this.subjectService.subjectWithStudentsGrade$;

  ngOnInit(): void {
    this.subjectService.getSubjectWithStudentsGrade(this.id);
  }

  editGrade(event: any, gradeId: number) {
    event.preventDefault();
    const formData = new FormData(event.target);
    const grade = Object.fromEntries(formData) as unknown as Grade;

    this.subjectService.editGrade(gradeId, grade).subscribe({
      next: (res) => {
        if (res.status) {
          this.subjectService.getSubjectWithStudentsGrade(this.id);
          this.notificationService.alertSuccess(res.message);
          return;
        }
        this.notificationService.alertError(res.message);
      },
      error: (err) => {
        const error = err.error as BaseResponse<null>;
        this.notificationService.alertError(error.message);
      },
    });
  }

  assignGrade(event: any, studentId: number, subjectId: number) {
    event.preventDefault();
    const formData = new FormData(event.target);
    const { value, note } = Object.fromEntries(formData) as unknown as Grade;

    this.subjectService
      .assignGrade({
        value: value ? + value : 0,
        note,
        studentId,
        subjectId,
      })
      .subscribe({
        next: (res) => {
          if (res.status) {
            this.subjectService.getSubjectWithStudentsGrade(this.id);
            this.notificationService.alertSuccess(res.message);
            return;
          }
          this.notificationService.alertError(res.message);
        },
        error: (err) => {
          const error = err.error as BaseResponse<null>;
          this.notificationService.alertError(error.message);
        },
      });
  }
}

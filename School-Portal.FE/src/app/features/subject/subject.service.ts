import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, catchError, filter, map, of, switchMap } from 'rxjs';
import { BaseResponse } from '../../shared/models/base-model';

interface Subject {
  id: number;
  name: string;
  isRequired: boolean;
  minimumPassMark: number;
}

interface SubjectWithStudentsGrade extends Subject {
  students: Student[];
}

interface Student {
  id: number;
  name: string;
  grade: GradeAPI | null;
}

interface GradeAPI extends FullGrade {
  id: number;
}

export interface Grade {
  value: number;
  note: string;
}

export interface FullGrade extends Grade {
  studentId: number;
  subjectId: number;
}

@Injectable({
  providedIn: 'root',
})
export class SubjectService {
  private http = inject(HttpClient);

  subjectsResponse$ = this.http
    .get<BaseResponse<Subject[]>>(`/api/v1/subject`)
    .pipe(
      catchError(() => of(null)),
      map((res) => res?.data ?? [])
    );

  private _subjectWithStudentsGrade$ = new BehaviorSubject<number | null>(null);
  getSubjectWithStudentsGrade(subjectId: number) {
    this._subjectWithStudentsGrade$.next(subjectId);
  }
  subjectWithStudentsGrade$ = this._subjectWithStudentsGrade$.pipe(
    filter((res) => Boolean(res)),
    switchMap((subjectId) => {
      return this.http
        .get<BaseResponse<SubjectWithStudentsGrade>>(
          `/api/v1/subject/${subjectId}/students/grade`
        )
        .pipe(
          catchError((err: HttpErrorResponse) => {
            return of(err.error as BaseResponse<SubjectWithStudentsGrade>);
          })
        );
    })
  );
  assignGrade(grade: FullGrade) {
    return this.http.post<BaseResponse<null>>(`/api/v1/grade`, grade);
  }

  editGrade(id: number, grade: Grade) {
    return this.http.patch<BaseResponse<null>>(`/api/v1/grade/${id}`, grade);
  }
}

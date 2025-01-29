import { Component, inject } from '@angular/core';
import { SubjectService } from '../subject.service';

@Component({
  selector: 'app-subject-list',
  standalone: false,
  templateUrl: './subject-list.component.html',
  styleUrl: './subject-list.component.scss'
})
export class SubjectListComponent {

  private subjectService = inject(SubjectService);

  subjects$ = this.subjectService.subjectsResponse$; 
}

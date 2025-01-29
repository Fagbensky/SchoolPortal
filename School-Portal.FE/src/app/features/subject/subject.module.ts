import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SubjectRoutingModule } from './subject-routing.module';
import { SubjectListComponent } from './subject-list/subject-list.component';
import { SubjectDetailsComponent } from './subject-details/subject-details.component';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NotFoundComponent } from '../../shared/components/not-found/not-found.component';

@NgModule({
  declarations: [SubjectListComponent, SubjectDetailsComponent],
  imports: [
    CommonModule,
    SubjectRoutingModule,
    MatIconModule,
    MatExpansionModule,
    MatInputModule,
    MatButtonModule,
    NotFoundComponent,
  ],
})
export class SubjectModule {}

import { Component, inject } from '@angular/core';
import { NotFoundComponent } from '../../shared/components/not-found/not-found.component';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, NotFoundComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  router = inject(Router)

  routeToSubjects(){
    this.router.navigateByUrl("/subject")
  }
}

import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { UtilityService } from '../../core/services/utility.service';
import { Observable, tap } from 'rxjs';

@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [CommonModule, RouterModule, MatSidenavModule, MatIconModule],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.scss',
})
export class NavigationComponent {
  isMobile: boolean = false;
  hasBackdrop = false;
  mode: any = 'side';
  opened = true;

  readonly routes = [
    {
      label: 'Home',
      route: ['/home'],
    },
    {
      label: 'Subject',
      route: ['/subject'],
    },
  ];

  isMobile$: Observable<boolean>;

  constructor(private utilityService: UtilityService) {
    this.isMobile$ = this.utilityService.isMobile$.pipe(
      tap((res: any) => {
        this.isMobile = res;
        this.hasBackdrop = res;
        this.mode = res ? 'over' : 'side';
        this.opened = !res;
      })
    );
  }

  mobileCLoseNav() {
    if (this.isMobile) {
      this.opened = false;
    }
  }
}

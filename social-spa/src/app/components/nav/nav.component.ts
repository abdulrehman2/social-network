import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { MenuItem } from 'primeng/api/menuitem';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user-service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
  providers: [MessageService, ConfirmationService],
})
export class NavComponent implements OnInit {
  userSearchResults: any;
  selectedUser: any;
  items: MenuItem[] = new Array<MenuItem>();
  user: User;
  constructor(private userService: UserService, private router: Router) {
    this.user = userService.getUserModel();
  }

  ngOnInit(): void {
    this.items = [
      {
        label: 'Sign Out',
        icon: 'pi pi-fw pi-sign-out',
        command: (event: Event) => {
          this.router.navigate(['/login']);
        },
      },
      {
        label: 'Profile',
        icon: 'pi pi-fw pi-user',
        command: (event: Event) => {
          this.router.navigate(['/home/user-profile']);
        },
      },
    ];
  }
  search(event: any): void {
    let query = event.query;
    this.userService.searchUser(query).subscribe(
      (response: any) => {
        this.userSearchResults = response;
      },
      (error: any) => {}
    );
  }

  openUser(): void {
    debugger;
    this.router.navigate(['/home/user-profile', { id: this.selectedUser.id }]);
  }
}

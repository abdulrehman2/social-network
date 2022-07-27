import { Component, Input, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { Friend } from 'src/app/models/friend';
import { FriendService } from 'src/app/services/friend-service';
import { UserService } from 'src/app/services/user-service';

@Component({
  selector: 'app-user-friends',
  templateUrl: './user-friends.component.html',
  styleUrls: ['./user-friends.component.scss'],
})
export class UserFriendsComponent implements OnInit {
  public friends: Array<Friend> = new Array<Friend>();
  sortOptions: SelectItem[];
  sortOrder: number;
  sortField: string;
  sortKey: string;
  @Input() userId: number;
  constructor(
    private _friendService: FriendService,
    private userService: UserService
  ) {
    this.sortOptions = [
      { label: 'New To Oldest', value: '!friendSince' },
      { label: 'Oldest to New', value: 'friendSince' },
    ];
  }

  ngOnInit(): void {
    if (this.userId == null || this.userId === undefined) {
      this.userId = this.userService.getUserModel().id;
    }
    this.getUserFriends(this.userId);
  }

  onSortChange(event: any) {
    let value = event.value;

    if (value.indexOf('!') === 0) {
      this.sortOrder = -1;
      this.sortField = value.substring(1, value.length);
    } else {
      this.sortOrder = 1;
      this.sortField = value;
    }
  }

  getUserFriends(userId: any) {
    this._friendService.getFriends(userId).subscribe(
      (response: Array<Friend>) => {
        debugger;
        this.friends = response;
      },
      (error: any) => {
        debugger;
      }
    );
  }
}

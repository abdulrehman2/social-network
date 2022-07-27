import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { FriendSuggestion } from 'src/app/models/friend-suggestion';
import { FriendService } from 'src/app/services/friend-service';

@Component({
  selector: 'app-suggested-friends',
  templateUrl: './suggested-friends.component.html',
  styleUrls: ['./suggested-friends.component.scss'],
})
export class SuggestedFriendsComponent implements OnInit {
  constructor(
    private friendService: FriendService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}
  suggestions: Array<FriendSuggestion>;
  ngOnInit(): void {
    this.getFriendSuggestions();
  }

  getFriendSuggestions() {
    this.friendService.getFriendSuggestions().subscribe(
      (response: FriendSuggestion[]) => {
        debugger;
        this.suggestions = response;
      },
      (error: any) => {}
    );
  }

  sendFriendRequest(newFriendRequest: FriendSuggestion) {
    var requestedUser = newFriendRequest;
    this.confirmationService.confirm({
      //target: event.target,
      message: `Are you sure that you want to send friend request to ${requestedUser.name} ?`,
      key: 'baseConfirmDialog',
      accept: () => {
        this.friendService
          .addFriendRequest({ requestedUserId: newFriendRequest.userId })
          .subscribe(
            (response: any) => {
              this.suggestions = this.suggestions.filter(
                (suggestion) => suggestion.userId != newFriendRequest.userId
              );
              this.messageService.add({
                key: 'layout',
                severity: 'success',
                summary: response.message,
              });
            },
            (error: any) => {
              this.messageService.add({
                key: 'layout',
                severity: 'error',
                summary: error.error.message,
              });
            }
          );
      },
    });
  }
}

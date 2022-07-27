import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { FriendRequest } from 'src/app/models/friend-request';
import { FriendService } from 'src/app/services/friend-service';

@Component({
  selector: 'app-pending-requests',
  templateUrl: './pending-requests.component.html',
  styleUrls: ['./pending-requests.component.scss'],
})
export class PendingRequestsComponent implements OnInit {
  constructor(
    private friendService: FriendService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  pendingRequests: FriendRequest[];
  ngOnInit(): void {
    this.getPendingFriendRequests();
  }

  getPendingFriendRequests() {
    this.friendService.getPendingRequests().subscribe(
      (response: any) => {
        this.pendingRequests = response.data;
      },
      (error: any) => {}
    );
  }

  acceptRequest(request: any) {
    this.friendService
      .acceptFriendRequest({ friendRequestId: request.requestId })
      .subscribe(
        (response: any) => {
          this.pendingRequests = this.pendingRequests.filter(
            (p) => p.requestId != request.requestId
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
  }
  deleteRequest(request: any) {
    this.friendService.acceptFriendRequest(request).subscribe(
      (response: any) => {},
      (error: any) => {}
    );
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FriendRequest } from '../models/friend-request';

@Injectable({
  providedIn: 'root',
})
export class FriendService {
  private baseUrl = `${environment.apiURL}/api/identity/friends`;

  constructor(private http: HttpClient) {}

  //headers: { 'Content-Type': 'multipart/form-data' }

  public getFriendSuggestions(): any {
    return this.http.get<any>(this.baseUrl + '/suggestions');
  }

  public getFriends(userId: any): any {
    return this.http.get<any>(this.baseUrl + '/friends/' + userId);
  }

  public getPendingRequests(): any {
    return this.http.get<Array<FriendRequest>>(
      this.baseUrl + '/pending_requests'
    );
  }

  public addFriendRequest(requestModel: any): any {
    return this.http.post<any>(
      this.baseUrl + '/add_friend_request',
      requestModel
    );
  }

  public acceptFriendRequest(requestModel: any): any {
    return this.http.post<any>(this.baseUrl + '/accept_request', requestModel);
  }

  public deleteFriendRequest(requestModel: any): any {
    return this.http.post<any>(this.baseUrl + '/delete_request', requestModel);
  }
}

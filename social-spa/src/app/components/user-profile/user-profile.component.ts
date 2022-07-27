import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/models/post';
import { User } from 'src/app/models/user';
import { PostService } from 'src/app/services/post-service';
import { UserService } from 'src/app/services/user-service';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
})
export class UserProfileComponent implements OnInit {
  public userPosts: Array<Post> = [];
  public user: User;
  public profileId: any;
  isPopupCreatePostVisisble: boolean = false;
  constructor(
    private postService: PostService,
    private userService: UserService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    debugger;
    this.profileId = this.activatedRoute.snapshot.paramMap.get('id');

    if (this.profileId) {
      this.getUserPosts(this.profileId);
      this.getUserProfile(this.profileId);
    } else {
      this.user = this.userService.getUserModel();
      this.getUserPosts(this.user.id);
      this.getUserProfile(this.user.id);
    }
  }

  showAddPostDialog(): void {
    this.isPopupCreatePostVisisble = true;
  }

  public reciveCloseDialogNotification(currentValue: boolean): void {
    debugger;
    this.isPopupCreatePostVisisble = currentValue;
  }

  getUserPosts(userId: any) {
    this.postService.getPosts(userId).subscribe(
      (response: any) => {
        debugger;
        this.userPosts = response;
      },
      (error: any) => {}
    );
  }

  getUserProfile(userId: any) {
    this.userService.getUserDetails(userId).subscribe(
      (response: User) => {
        debugger;
        this.user = response;
      },
      (error: any) => {}
    );
  }
}

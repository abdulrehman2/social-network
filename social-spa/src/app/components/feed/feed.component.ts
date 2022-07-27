import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { Post } from 'src/app/models/post';
import { User } from 'src/app/models/user';
import { PostService } from 'src/app/services/post-service';
import { UserService } from 'src/app/services/user-service';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.scss'],
})
export class FeedComponent implements OnInit {
  public feedPosts: Array<Post> = [];

  public user: User;
  constructor(
    private userService: UserService,
    private postService: PostService
  ) {}

  ngOnInit(): void {
    this.user = this.userService.getUserModel();
    this.getUserWall();
  }

  getUserWall(): void {
    this.postService.getUserWall().subscribe(
      (response: any) => {
        debugger;
        this.feedPosts = response;
      },
      (error: any) => {}
    );
  }
}

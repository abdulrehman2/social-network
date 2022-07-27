import { Component, Input, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Post } from 'src/app/models/post';
import { ViewChild } from '@angular/core';
import { OverlayPanel } from 'primeng/overlaypanel';
import { ReactService } from 'src/app/services/react-service';
import { ReactToCreate } from 'src/app/models/reactToCreate';

@Component({
  selector: 'app-react-options',
  templateUrl: './react-options.component.html',
  styleUrls: ['./react-options.component.scss'],
})
export class ReactOptionsComponent implements OnInit {
  dockItems: MenuItem[];
  @Input() post: Post;
  reactTypeIcon: string;
  reactTypeLabel: string = 'Like';
  @ViewChild('op') overlayPanel: OverlayPanel;

  constructor(private reactService: ReactService) {}

  ngOnInit(): void {
    if (this.post.isReacted) {
      this.reactOnPost(this.post.reactTypeId);
    }
    this.dockItems = [
      {
        label: 'Like',
        icon: 'assets/reaction/like.png',
        command: (event: Event) => {
          if (!this.post.isReacted) {
            this.incrementLocalCount();
          }
          this.reactOnPost(1);
          this.syncReactToServer();
        },
      },
      {
        label: 'Heart',
        icon: 'assets/reaction/heart.png',
        command: (event: Event) => {
          if (!this.post.isReacted) {
            this.incrementLocalCount();
          }
          this.reactOnPost(2);
          this.syncReactToServer();
        },
      },
      {
        label: 'Laugh',
        icon: 'assets/reaction/smile.png',
        command: (event: Event) => {
          if (!this.post.isReacted) {
            this.incrementLocalCount();
          }
          this.reactOnPost(3);
          this.syncReactToServer();
        },
      },
      {
        label: 'Angry',
        icon: 'assets/reaction/angry.png',
        command: (event: Event) => {
          if (!this.post.isReacted) {
            this.incrementLocalCount();
          }
          this.reactOnPost(4);
          this.syncReactToServer();
        },
      },
    ];
  }

  viewReactOptions(event: any): any {
    setTimeout(() => {
      this.overlayPanel.show(event);
    }, 1000);
  }
  hideReactOptions(event: any): any {
    setTimeout(() => {
      this.overlayPanel.hide();
    }, 3000);
  }

  handleLike(e: any): any {
    let isChecked = e.checked;
    if (isChecked) {
      this.reactOnPost(1);
      this.incrementLocalCount();
    } else {
      this.reactOnPost(0);
      this.decrementLocalCount();
    }
    this.syncReactToServer();
  }

  reactOnPost(currentReactTypeId: number): void {
    debugger;
    this.post.reactTypeId = currentReactTypeId;
    this.post.isReacted = true;
    switch (currentReactTypeId) {
      case 1:
        this.reactTypeIcon = 'like-icon';
        this.reactTypeLabel = 'Like';
        break;
      case 2:
        this.reactTypeIcon = 'heart-icon';
        this.reactTypeLabel = 'Love';

        break;

      case 3:
        this.reactTypeIcon = 'smile-icon';
        this.reactTypeLabel = 'Haha';
        break;

      case 4:
        this.reactTypeIcon = 'angry-icon';
        this.reactTypeLabel = 'Angry';
        break;
      default:
        this.reactTypeLabel = 'Like';
        this.reactTypeIcon = 'pi pi-thumbs-up';
        this.post.isReacted = false;
        break;
    }
  }

  incrementLocalCount(): void {
    //if (this.post.isReacted == false) {
    this.post.reactCount++;
    //}
  }
  decrementLocalCount(): void {
    //if (this.post.isReacted == false) {
    this.post.reactCount--;
    //}
  }
  syncReactToServer(): void {
    let model: ReactToCreate = {
      postId: this.post.id,
      reactTypeId: this.post.reactTypeId,
    };
    this.reactService.addReact(model).subscribe(
      (response: any) => {},
      (error: any) => {}
    );
  }
}

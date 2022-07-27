export interface Post {
  id: number;
  createdDate?: string;
  writtenText?: string;
  mediaLocation?: string;
  postCreator?: string;
  postCreatorId?: string;
  postCreatorProfilePicutre?: string;
  reactCount?: number;
  commentCount?: number;
  isReacted: boolean;
  reactTypeId: number;
}

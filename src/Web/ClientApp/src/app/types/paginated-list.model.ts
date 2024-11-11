export interface PaginatedListModel<T> {
  items: T[];
  pageNumber: number;
  totalPages: number;
  totalCount: number;
}

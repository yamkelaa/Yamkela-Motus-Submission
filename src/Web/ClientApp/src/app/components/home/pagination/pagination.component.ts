import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { VehicleActions } from 'src/app/stores/vehicles/vehicle.actions';
import { VehicleState } from 'src/app/stores/vehicles/vehicle.state';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
})
export class PaginationComponent implements OnInit {
  @Input() pageSize: number;
  @Output() pageChanged = new EventEmitter<number>();

  public currentPage: number = 1;
  public totalPages: number = 1;
  public pageNumbers: number[] = [];

  constructor(private store: Store) { }

  ngOnInit(): void {
    this.store.select(VehicleState.getPagination).subscribe((paginationState) => {
      this.currentPage = paginationState.pageNumber;
      this.totalPages = paginationState.totalPages;
      this.updatePageNumbers();
    });
  }

  updatePageNumbers(): void {
    this.pageNumbers = [];
    const startPage = Math.max(this.currentPage - 1, 1);
    const endPage = Math.min(this.currentPage + 2, this.totalPages);
    for (let i = startPage; i <= endPage; i++) {
      this.pageNumbers.push(i);
    }
  }

  changePage(page: number): void {
    if (page !== this.currentPage) {
      this.store.dispatch(new VehicleActions.SetPagination(page, this.totalPages, 0, this.pageSize));
      this.pageChanged.emit(page);
    }
  }

  goToFirstPage(): void {
    if (this.currentPage > 1) {
      this.changePage(1);
    }
  }

  goToPreviousPage(): void {
    if (this.currentPage > 1) {
      this.changePage(this.currentPage - 1);
    }
  }

  goToNextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.changePage(this.currentPage + 1);
    }
  }

  goToLastPage(): void {
    if (this.currentPage < this.totalPages) {
      this.changePage(this.totalPages);
    }
  }

  isFirstPageDisabled(): boolean {
    return this.currentPage <= 1;
  }

  isPreviousPageDisabled(): boolean {
    return this.currentPage <= 1;
  }

  isNextPageDisabled(): boolean {
    return this.currentPage >= this.totalPages;
  }

  isLastPageDisabled(): boolean {
    return this.currentPage >= this.totalPages;
  }
}

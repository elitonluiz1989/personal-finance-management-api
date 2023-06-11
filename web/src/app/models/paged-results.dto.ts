import { PaginationDto } from "./pagination.dto";

export class PagedResultsDto<T> {
  public results: T[] = [];
  public pagination = new PaginationDto();
}

import { Button } from "./ui/button"

interface PaginationProps {
  currentPage: number
  totalPages: number
  onPageChange: (page: number) => void
}

export function Pagination({ currentPage, totalPages, onPageChange }: PaginationProps) {
  return (
    <div className="flex justify-center gap-2 mt-4">
      <Button
        onClick={() => onPageChange(currentPage - 1)}
        disabled={currentPage === 1}
        // className="px-4 py-2 border rounded disabled:opacity-50"
      >
        Previous
      </Button>
      
      <span className="px-4 py-2">
        Page {currentPage} of {totalPages}
      </span>

      <Button
        onClick={() => onPageChange(currentPage + 1)}
        disabled={currentPage === totalPages}
        // className="px-4 py-2 border rounded disabled:opacity-50"
      >
        Next
      </Button>
    </div>
  )
}
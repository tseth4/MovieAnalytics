import BudgetChart from "./BudgetChart";
import NewBudgeChart from "./NewBudgetChart";
import TopProfitableMovie from "./TopProfitableMoviesChart";


export default function Analytics() {
  return (
    <div className="flex flex-col gap-10">
      <h1>Analytics</h1>
      <p>      Welcome to Movie Analytics!
        Data is based on top 500-600 Movies of each year from 1960 to 2024. <br></br>

       Data by  <strong>Raed Addala</strong> on <a href="https://www.kaggle.com/datasets/raedaddala/top-500-600-movies-of-each-year-from-1960-to-2024">kaggle</a></p>
      <NewBudgeChart />
      <TopProfitableMovie />
      {/* <BudgetChart/> */}

    </div>
  )
}
import React, { useState, useEffect, useMemo } from 'react';
import './App.css';
import {
  useReactTable,
  getCoreRowModel,
  flexRender,
  getSortedRowModel,
} from '@tanstack/react-table';
import { BiSortAlt2 } from "react-icons/bi";

const App = () => {
  const [currencies, setCurrencies] = useState([]);
  const [selectedCurrency, setSelectedCurrency] = useState('USD');
  const [exchangeRates, setExchangeRates] = useState([]);

  useEffect(() => {
    const fetchCurrencies = async () => {
      try {
        const response = await fetch('https://localhost:7011/api/ExchangeRates/currencies');
        const data = await response.json();
        setCurrencies(data);
      } catch (error) {
        console.error('Error fetching currencies:', error);
      }
    };

    fetchCurrencies();
  }, []);

  useEffect(() => {
    const fetchExchangeRates = async () => {
      try {
        const response = await fetch(`https://localhost:7011/api/ExchangeRates/rates/${selectedCurrency}`);
        const data = await response.json();
        setExchangeRates(data);
      } catch (error) {
        console.error('Error fetching exchange rates:', error);
      }
    };

    if (selectedCurrency) {
      fetchExchangeRates();
    }
  }, [selectedCurrency]);

  const columns = useMemo(
    () => [
      {
        accessorKey: 'baseCurrency',
        header: 'Base Currency',
        enableSorting: false,
        cell: info => <p>{info.getValue()}</p>,
      },
      {
        accessorKey: 'targetCurrency',
        header: 'Target Currency',
        enableSorting: true,
        cell: info => <p>{info.getValue()}</p>
      },
      {
        accessorKey: 'rate',
        header: 'Exchange Rate',
        enableSorting: true,
        cell: info => <p>{info.getValue().toFixed(2)}</p>
      }
    ],
    []
  );

  const table = useReactTable({
    data: exchangeRates,
    columns,
    getCoreRowModel: getCoreRowModel(),
    getSortedRowModel: getSortedRowModel(),
  });

  return (
    <div id="root">
      <h1>Exchange Rates</h1>
      <select className="select" onChange={(e) => setSelectedCurrency(e.target.value)} value={selectedCurrency}>
        {currencies.map((currency) => (
          <option key={currency.idCoin} value={currency.nameCoin}>
            {currency.nameCoin}
          </option>
        ))}
      </select>
      <div className="container">
        <table className="table">
          <thead>
            {table.getHeaderGroups().map((headerGroup) => (
              <tr key={headerGroup.id}>
                {headerGroup.headers.map(header => (
                  <th key={header.id}>
                    {header.isPlaceholder
                      ? null
                      : flexRender(header.column.columnDef.header, header.getContext())}
                    {header.column.getCanSort() && (
                      <BiSortAlt2 className='sort'
                        onClick={header.column.getToggleSortingHandler()}
                      />
                    )}
                    {{
                      asc: ' 🔼',
                      desc: ' 🔽',
                    }[header.column.getIsSorted()] ?? null}
                  </th>
                ))}
              </tr>
            ))}
          </thead>
          <tbody>
            {table.getRowModel().rows.map((row) => (
              <tr key={row.id}>
                {row.getVisibleCells().map((cell) => (
                  <td key={cell.id}>
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default App;

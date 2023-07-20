import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './App.css';
import TranslatorCard from './TranslatorCard';
import AddTranslatorForm from './AddTranslatorForm';

const API_BASE_URL = 'http://localhost:7729/api/translators';

interface TranslatorDto {
    id: number;
    name: string;
    hourlyRate: string;
    creditCardNumber: string;
    status: string;
}

const App = () => {
    const [translators, setTranslators] = useState<TranslatorDto[]>([]);

    useEffect(() => {
        axios.get<TranslatorDto[]>(API_BASE_URL).then((response) => {
            setTranslators(response.data);
        });
    }, []);

    const handleAddTranslator = (formData: any) => {
        axios.post(API_BASE_URL, formData).then(() => {
            // Reload translators after adding a new one
            axios.get<TranslatorDto[]>(API_BASE_URL).then((response) => {
                setTranslators(response.data);
            });
        });
    };

    return (
        <div className="App">
            <h1>Translators</h1>
            <div className="translator-cards">
                {translators.map((translator) => (
                    <TranslatorCard key={translator.id} translator={translator} />
                ))}
            </div>
            <h2>Add Translator</h2>
            <AddTranslatorForm onSubmit={handleAddTranslator} />
        </div>
    );
};

export default App;

import streamlit as st
import pandas as pd
import numpy as np
import plotly.graph_objects as go
from datetime import datetime

def main():
    # Título principal con emoji
    st.markdown("# 🤖 RULE EXTRACTOR")
    st.markdown("---")

    # Crear tabs
    tab_data, tab_features, tab_rules, tab_validation, tab_forward, tab_backtest = st.tabs([
    "Data", "Feature Selection", "Rule Extraction", "Validation", "Forward", "Backtest"
    ])

# TAB DATA
    with tab_data:
        uploaded_file = st.file_uploader(
            "Arrastra y suelta tu archivo CSV aquí",
            type=["csv"]
        )
        
        if uploaded_file is not None:
            try:
                # Leer el archivo y procesar datos
                df = pd.read_csv(uploaded_file)
                df = calculate_target(df)
                
                # Mostrar el DataFrame
                st.write("### Vista previa de los datos")
                st.dataframe(df)

                # Detectar columna de fecha
                date_columns = df.select_dtypes(include=['datetime64']).columns
                if len(date_columns) == 0:
                    potential_date_cols = [col for col in df.columns if 'date' in col.lower() or 'fecha' in col.lower()]
                    if potential_date_cols:
                        df[potential_date_cols[0]] = pd.to_datetime(df[potential_date_cols[0]])
                        date_column = potential_date_cols[0]
                    else:
                        st.error("No se encontró una columna de fecha. Por favor, asegúrate de que tu CSV tiene una columna de fecha.")
                        return
                else:
                    date_column = date_columns[0]

                # Obtener el rango de fechas
                min_date = pd.to_datetime(df[date_column].min())
                max_date = pd.to_datetime(df[date_column].max())
                
                st.write("### Selección de períodos")
                st.write(f"Rango de fechas disponible: {min_date.strftime('%Y-%m-%d')} a {max_date.strftime('%Y-%m-%d')}")

                # Fechas por defecto
                default_dates = {
                    'test_start': min_date,
                    'test_end': pd.to_datetime('2020-01-01'),
                    'train_start': pd.to_datetime('2020-01-01'),
                    'train_end': pd.to_datetime('2023-01-01'),
                    'forward_start': pd.to_datetime('2023-01-01'),
                    'forward_end': max_date
                }

                # Contenedor para los selectores de fecha
                with st.container():
                    col1, col2, col3 = st.columns(3)

                    with col1:
                        st.markdown("### Test")
                        test_start = st.date_input(
                            "Inicio Test",
                            default_dates['test_start'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="test_start"
                        )
                        test_end = st.date_input(
                            "Fin Test",
                            default_dates['test_end'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="test_end"
                        )

                    with col2:
                        st.markdown("### Train")
                        train_start = st.date_input(
                            "Inicio Train",
                            default_dates['train_start'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="train_start"
                        )
                        train_end = st.date_input(
                            "Fin Train",
                            default_dates['train_end'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="train_end"
                        )

                    with col3:
                        st.markdown("### Forward")
                        forward_start = st.date_input(
                            "Inicio Forward",
                            default_dates['forward_start'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="forward_start"
                        )
                        forward_end = st.date_input(
                            "Fin Forward",
                            default_dates['forward_end'].date(), 
                            min_value=min_date.date(), 
                            max_value=max_date.date(), 
                            key="forward_end"
                        )

                # Convertir todas las fechas a datetime para las máscaras
                test_start_dt = pd.to_datetime(test_start)
                test_end_dt = pd.to_datetime(test_end)
                train_start_dt = pd.to_datetime(train_start)
                train_end_dt = pd.to_datetime(train_end)
                forward_start_dt = pd.to_datetime(forward_start)
                forward_end_dt = pd.to_datetime(forward_end)

                # Crear y guardar todas las máscaras en session_state
                st.session_state['mask_train'] = (df[date_column] >= train_start_dt) & (df[date_column] <= train_end_dt)
                st.session_state['mask_test'] = (df[date_column] >= test_start_dt) & (df[date_column] <= test_end_dt)
                st.session_state['mask_forward'] = (df[date_column] >= forward_start_dt) & (df[date_column] <= forward_end_dt)

                train_returns = df[st.session_state['mask_train']]['Target']
                test_returns = df[st.session_state['mask_test']]['Target']
                forward_returns = df[st.session_state['mask_forward']]['Target']

                # Guardar los returns en session_state
                st.session_state['train_returns'] = train_returns
                st.session_state['test_returns'] = test_returns
                st.session_state['forward_returns'] = forward_returns

                # Mostrar gráfico de evolución de precios
                st.write("### Evolución del precio")
                price_fig = plot_price_evolution(
                    df, 
                    date_column,
                    train_start,
                    train_end,
                    test_start,
                    test_end,
                    forward_start,
                    forward_end
                )
                st.plotly_chart(price_fig)

                # Estadísticas descriptivas
                st.write("### Estadísticas de los retornos")
                stats_df = create_returns_stats_table(train_returns, test_returns, forward_returns)
                st.dataframe(stats_df, use_container_width=True)

# Test KS y su visualización
                st.write("### Comparación de Distribuciones Train-Test")
                ks_fig, ks_stat, p_value = plot_ks_test(train_returns, test_returns)
                st.plotly_chart(ks_fig)

                if p_value < 0.05:
                    st.warning("""
                        ⚠️ Las distribuciones de train y test son significativamente diferentes.
                        Los resultados de la validación podrían no ser representativos.
                    """)

                # Añadir waterfall plot para train+test combinados
                st.write("### Análisis de Magnitud de Retornos (Train + Test)")
                combined_returns = pd.concat([train_returns, test_returns])
                waterfall_fig = plot_returns_waterfall(combined_returns)
                st.plotly_chart(waterfall_fig)

                # Calcular el retorno neto para las conclusiones
                net_return = combined_returns.sum()

                # Añadir conclusiones
                st.write("### Conclusiones del Análisis")
                
                st.write("📊 **Estabilidad del Activo**")
                if p_value > 0.60:
                    st.success("🌟 **Activo Muy Adecuado para Minería de Reglas**\n\n"
                             "Las distribuciones de train y test son altamente similares, "
                             "lo que sugiere una gran estabilidad en el comportamiento del activo.")
                elif p_value > 0.30:
                    st.info("✅ **Activo Adecuado para Minería de Reglas**\n\n"
                          "Existe una similitud razonable entre las distribuciones de train y test, "
                          "permitiendo la búsqueda de reglas con cierta confianza.")
                else:
                    st.warning("⚠️ **Activo Poco Adecuado para Minería de Reglas**\n\n"
                             "Las diferencias significativas entre train y test sugieren "
                             "inestabilidad en el comportamiento del activo.")

                st.write("📈 **Comportamiento Tendencial**")
                if net_return > 15:
                    st.info("📈 **Sesgo Alcista Significativo**\n\n"
                          "El activo muestra una fuerte tendencia alcista en el período analizado, "
                          "con un retorno neto superior al 15%.")
                elif net_return < -15:
                    st.info("📉 **Sesgo Bajista Significativo**\n\n"
                          "El activo muestra una fuerte tendencia bajista en el período analizado, "
                          "con un retorno neto inferior al -15%.")
                else:
                    st.info("↔️ **Sin Sesgo Tendencial Claro**\n\n"
                          "El activo no muestra un sesgo tendencial significativo, "
                          "con un retorno neto entre -15% y 15%.")

                # Guardar variables en session_state para usar en otras tabs
                st.session_state['df'] = df
                st.session_state['date_column'] = date_column

            except Exception as e:
                st.error(f"Error al procesar el archivo: {str(e)}")

# TAB FEATURE SELECTION
    with tab_features:
        if 'df' in st.session_state:
            st.subheader("Selección de Features")
            
            col1, col2 = st.columns(2)
            
            with col1:
                # Selector para side
                side = st.selectbox(
                    'Selecciona Side',
                    options=['long', 'short'],
                    help='Dirección de la estrategia'
                )
            
            with col2:
                # Selector para umbral de correlación
                correlation_threshold = st.slider(
                    'Umbral de Correlación',
                    min_value=0.75,
                    max_value=1.0,
                    value=0.95,
                    step=0.01,
                    help='Umbral máximo de correlación permitido entre features. Un valor más bajo resulta en menos features pero más independientes entre sí.'
                )
            
            # Calcular todas las features solo para el período de train
            if st.button('Analizar Features'):
                with st.spinner('Analizando features...'):
                    # Obtener features y sus scores
                    features_df, rules_dict = analyze_all_features(
                        st.session_state['df'][st.session_state['mask_train']], 
                        'Target',
                        st.session_state['date_column'],
                        side,
                        correlation_threshold
                    )
                    
                    # Guardar reglas en session state para Rule Extraction
                    st.session_state['primary_rules'] = rules_dict
                    st.session_state['side'] = side
                    
                    if not features_df.empty:
                        st.write(f"Features seleccionadas después de aplicar el filtro de correlación (umbral: {correlation_threshold}):")
                        st.dataframe(features_df, use_container_width=True)
                    else:
                        st.warning("No se encontraron features significativas")
        else:
            st.info("Por favor, carga un archivo CSV en la pestaña Data primero.")

    # TAB RULE EXTRACTION
    with tab_rules:
        if 'df' in st.session_state and 'primary_rules' in st.session_state:
            st.subheader("Extracción de Reglas Compuestas")
            
            # Calcular monkey test solo la primera vez
            if 'compound_threshold' not in st.session_state:
                with st.spinner('Calculando distribución de monos...'):
                    random_metrics = calculate_random_metrics_compound(
                        st.session_state['train_returns'],
                        st.session_state['side']
                    )
                    threshold = np.quantile(random_metrics, 0.99)
                    st.session_state['compound_threshold'] = threshold
                    st.session_state['random_metrics'] = random_metrics
            
            # Mostrar distribución de monos
            st.plotly_chart(plot_monkey_distribution(
                st.session_state['random_metrics'],
                st.session_state['compound_threshold']
            ))
            
            # Obtener features que pasaron el filtro inicial
            features_df = pd.DataFrame([
                {'feature': k, 'score': v['score']} 
                for k, v in st.session_state['primary_rules'].items()
            ]).sort_values('score', ascending=False)
            
            # Lista de todas las features filtradas
            filtered_features = features_df['feature'].tolist()
            
            selected_feature = st.selectbox(
                'Selecciona una feature base:',
                options=filtered_features,
                format_func=lambda x: f"{x} (Score: {features_df[features_df['feature'] == x]['score'].iloc[0]:.2f})"
            )
            
            # Mostrar cuántas features hay disponibles para la segunda regla
            st.info(f"Se buscarán reglas compuestas usando las {len(filtered_features)-1} features restantes que pasaron el filtro inicial.")
            
            # Botón para buscar reglas
            if st.button('Buscar Reglas'):
                with st.spinner('Buscando reglas compuestas...'):
                    # Obtener regla base
                    base_rule = st.session_state['primary_rules'][selected_feature]['rule']
                    
                    # Encontrar reglas compuestas
                    compound_rules_df = find_second_rule(
                        st.session_state['df'][st.session_state['mask_train']],
                        selected_feature,
                        base_rule,
                        'Target',
                        st.session_state['date_column'],
                        st.session_state['side'],
                        st.session_state['compound_threshold'],
                        filtered_features  # Pasamos la lista de features filtradas
                    )
                    
                    # Guardar reglas en session_state
                    st.session_state['compound_rules_df'] = compound_rules_df
            
            # Mostrar reglas y evolución si existen
            if 'compound_rules_df' in st.session_state:
                compound_rules_df = st.session_state['compound_rules_df']
                if compound_rules_df is not None and not compound_rules_df.empty:
                    st.dataframe(compound_rules_df, use_container_width=True)
                    
                    # Selector de regla para visualización
                    selected_rule_index = st.selectbox(
                        'Selecciona una regla para ver su evolución:',
                        options=range(len(compound_rules_df)),
                        format_func=lambda x: f"Métrica: {compound_rules_df.iloc[x]['metric']:.3f} | {compound_rules_df.iloc[x]['rule']}"
                    )
                    
                    # En Rule Extraction, mostrar evolución de la regla seleccionada
                    rule = compound_rules_df.iloc[selected_rule_index]['rule']
                    fig = plot_rule_returns(
                        st.session_state['df'][st.session_state['mask_train']],
                        rule,
                        st.session_state['date_column'],
                        st.session_state['side'],
                        st.session_state['mask_train']  # Añadido el nuevo parámetro
                    )
                    
                    if fig is not None:
                        st.plotly_chart(fig)
                    else:
                        st.warning("No hay suficientes datos para mostrar la evolución")
                else:
                    st.warning("No se encontraron reglas compuestas que superen el umbral")
        else:
            st.info("Por favor, completa el análisis de features primero")

# TAB VALIDATION
    with tab_validation:
        # Verificar que tenemos todos los datos necesarios
        required_keys = ['df', 'compound_rules_df', 'mask_test', 'test_returns', 'side']
        missing_keys = [key for key in required_keys if key not in st.session_state]
        
        if missing_keys:
            st.info(f"Por favor, completa los pasos anteriores primero. Faltan los siguientes datos: {', '.join(missing_keys)}")
        else:
            # Calcular monkey test para test solo la primera vez
            if 'test_random_metrics' not in st.session_state:
                with st.spinner('Calculando distribución de monos para test...'):
                    df_test = st.session_state['df'][st.session_state['mask_test']]
                    test_returns = df_test['Target']
                    
                    random_metrics = calculate_random_metrics_compound(
                        test_returns,
                        st.session_state['side']
                    )
                    st.session_state['test_random_metrics'] = random_metrics
            
            # Validar reglas si existen
            compound_rules_df = st.session_state['compound_rules_df']
            if compound_rules_df is not None and not compound_rules_df.empty:
                with st.spinner('Validando reglas...'):
                    df_test = st.session_state['df'][st.session_state['mask_test']]
                    
                    # Validar las reglas
                    validation_df = validate_rules(
                        df_test,
                        compound_rules_df,
                        'Target',
                        st.session_state['side'],
                        st.session_state['test_random_metrics']
                    )
                    
                    # Preparar DataFrame para mostrar
                    display_df = validation_df[['rule', 'validation']].copy()
                    display_df['validation'] = display_df['validation'].round(2)
                    display_df = display_df.sort_values('validation', ascending=False)

                    # Guardar en session_state para usar en Forward
                    st.session_state['validation_df'] = display_df
                    
                    # Mostrar tabla
                    st.dataframe(display_df, use_container_width=True)
                    
                    # Permitir seleccionar una regla para ver su evolución en test
                    if len(display_df) > 0:
                        selected_rule = st.selectbox(
                            'Selecciona una regla para ver su evolución en test:',
                            options=display_df['rule'].tolist()
                        )
                        
                        # Obtener datos de train y test
                        mask_combined = st.session_state['mask_train'] | st.session_state['mask_test']
                        df_combined = st.session_state['df'][mask_combined]
                        
                        # Mostrar evolución de la regla seleccionada
                        fig = plot_rule_returns(
                            df_combined,
                            selected_rule,
                            st.session_state['date_column'],
                            st.session_state['side'],
                            st.session_state['mask_train']
                        )
                        
                        if fig is not None:
                            st.plotly_chart(fig)
                        else:
                            st.warning("No hay suficientes datos para mostrar la evolución")
            else:
                st.warning("No hay reglas para validar. Por favor, genera reglas en la pestaña Rule Extraction primero.")

# TAB FORWARD
    with tab_forward:
        # Verificar datos necesarios
        required_keys = ['df', 'validation_df', 'mask_forward']
        missing_keys = [key for key in required_keys if key not in st.session_state]
        
        if missing_keys:
            st.info(f"Por favor, completa los pasos anteriores primero. Faltan los siguientes datos: {', '.join(missing_keys)}")
        else:
            # Selector para el umbral de validación
            validation_threshold = st.slider(
                'Umbral de Validación',
                min_value=0.0,
                max_value=100.0,
                value=90.0,
                step=5.0,
                help='Selecciona el umbral mínimo de validación para filtrar las reglas'
            )
            
            # Guardar el umbral en session_state
            st.session_state['validation_threshold'] = validation_threshold
            
            # Filtrar reglas que superan el umbral
            filtered_rules = st.session_state['validation_df'][
                st.session_state['validation_df']['validation'] >= validation_threshold
            ].copy()
            
            if not filtered_rules.empty:
                st.write(f"Reglas que superan el umbral de validación ({validation_threshold}%):")
                st.dataframe(filtered_rules[['rule', 'validation']].sort_values('validation', ascending=False), use_container_width=True)
                
                # Selector de regla para visualización
                selected_rule = st.selectbox(
                    'Selecciona una regla para ver su evolución en forward:',
                    options=filtered_rules['rule'].tolist()
                )
                
                # Mostrar evolución de la regla seleccionada en forward
                fig = plot_rule_returns(
                    st.session_state['df'][st.session_state['mask_forward']],
                    selected_rule,
                    st.session_state['date_column'],
                    st.session_state['side'],
                    pd.Series(False, index=st.session_state['df'][st.session_state['mask_forward']].index)
                )
                
                if fig is not None:
                    st.plotly_chart(fig)
                else:
                    st.warning("No hay suficientes datos para mostrar la evolución")
            else:
                st.warning(f"No hay reglas que superen el umbral de validación de {validation_threshold}%")

# TAB BACKTEST
    with tab_backtest:
        # Verificar datos necesarios
        required_keys = ['df', 'validation_df', 'date_column', 'side', 'validation_threshold']
        missing_keys = [key for key in required_keys if key not in st.session_state]
        
        if missing_keys:
            st.info("Por favor, selecciona primero un umbral de validación en la pestaña Forward")
        else:
            # Usar el umbral definido en Forward
            validation_threshold = st.session_state['validation_threshold']
            filtered_rules = st.session_state['validation_df'][
                st.session_state['validation_df']['validation'] >= validation_threshold
            ]
            
            if not filtered_rules.empty:
                st.write(f"### Reglas que superan el umbral de validación ({validation_threshold}%)")
                
                # Seleccionar regla
                selected_rule = st.selectbox(
                    'Selecciona una regla para el backtest:',
                    options=filtered_rules['rule'].tolist()
                )
                
                # Obtener datos completos
                mask_all = st.session_state['mask_train'] | st.session_state['mask_test'] | st.session_state['mask_forward']
                df_all = st.session_state['df'][mask_all].copy()
                
                # Calcular métricas
                metrics, trades = calculate_backtest_metrics(
                    df_all, 
                    selected_rule, 
                    st.session_state['side']
                )
                
                if metrics:
                    # Mostrar métricas en dos columnas
                    col1, col2 = st.columns(2)
                    
                    with col1:
                        st.write("### Métricas de Trading")
                        st.write(f"Número de trades: {metrics['total_trades']}")
                        st.write(f"Retorno total: {metrics['total_return']:.2f}%")
                        st.write(f"Retorno medio: {metrics['avg_return']:.2f}%")
                        st.write(f"Ratio de aciertos: {metrics['win_rate']:.1f}%")
                    
                    with col2:
                        st.write("### Métricas de Riesgo")
                        st.write(f"Sharpe ratio: {metrics['sharpe']:.2f}")
                        st.write(f"Máximo drawdown: {metrics['max_drawdown']:.2f}%")
                        st.write(f"Mejor trade: {metrics['best_trade']:.2f}%")
                        st.write(f"Peor trade: {metrics['worst_trade']:.2f}%")
                    
                    # Mostrar gráficos
                    st.write("### Equity Curve")
                    equity_fig = plot_backtest_equity_curve(trades, st.session_state['date_column'])
                    st.plotly_chart(equity_fig)
                    
                    st.write("### Distribución de Retornos")
                    dist_fig = plot_trades_distribution(trades)
                    st.plotly_chart(dist_fig)
                    
                else:
                    st.warning("No hay suficientes trades para calcular las métricas")
            else:
                st.warning(f"No hay reglas que superen el umbral de validación de {validation_threshold}%")

if __name__ == "__main__":
    main()